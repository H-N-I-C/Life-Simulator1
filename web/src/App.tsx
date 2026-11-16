import { useCallback, useEffect, useState } from 'react'
import { Button } from '@/components/ui/button'
import StatBar from '@/components/game/StatBar'
import LifeLog from '@/components/game/LifeLog'
import ScenarioDialog from '@/components/game/ScenarioDialog'
import { ActivitiesPanel, CareerPanel, RelationshipsPanel } from '@/components/game/Panels'
import { loadGameData, type GameData } from '@/game/data'
import { ageUp, applyForJob, doActivity, fmtMoney, newLife, quitJob, resolveChoice, spendTime } from '@/game/engine'
import type { LifeState, Scenario } from '@/game/types'

type Tab = 'life' | 'activities' | 'career' | 'relationships'
const SAVE_KEY = 'lifesim.save.v1'

export default function App() {
  const [data, setData] = useState<GameData | null>(null)
  const [error, setError] = useState<string | null>(null)
  const [life, setLife] = useState<LifeState | null>(null)
  const [scenario, setScenario] = useState<Scenario | null>(null)
  const [tab, setTab] = useState<Tab>('life')

  useEffect(() => {
    loadGameData()
      .then((d) => {
        setData(d)
        const saved = localStorage.getItem(SAVE_KEY)
        if (saved) {
          try { setLife(JSON.parse(saved)) } catch { /* corrupted save */ }
        }
      })
      .catch((e) => setError(String(e)))
  }, [])

  useEffect(() => {
    if (life) localStorage.setItem(SAVE_KEY, JSON.stringify(life))
  }, [life])

  const start = useCallback(() => {
    if (!data) return
    setLife(newLife(data))
    setScenario(null)
    setTab('life')
  }, [data])

  const onAgeUp = () => {
    if (!life || !data || life.dead) return
    const { state, scenario: sc } = ageUp(life, data)
    setLife(state)
    if (sc) setScenario(sc)
  }

  const onActivity = (a: string) => {
    if (!life || !data) return
    const { state, scenario: sc } = doActivity(life, data, a)
    setLife(state)
    if (sc) setScenario(sc)
  }

  const onChoose = (i: number) => {
    if (!life || !scenario) return
    setLife(resolveChoice(life, scenario, scenario.choices[i]))
    setScenario(null)
  }

  if (error) return <div className="flex min-h-dvh items-center justify-center p-8 text-center text-rose-700">{error}</div>
  if (!data) return <div className="flex min-h-dvh items-center justify-center text-slate-500">Loading game data…</div>

  if (!life) {
    return (
      <div className="flex min-h-dvh flex-col items-center justify-center gap-6 bg-[#14213d] px-8 text-center">
        <h1 className="text-4xl font-black tracking-tight text-[#f8f9fa]">Life Simulator</h1>
        <p className="max-w-sm text-base leading-relaxed text-slate-300">
          A web port built on the deobfuscated game data of BitLife 3.26.1 — real scenarios, careers and life events.
        </p>
        <Button size="lg" className="bg-[#fcbf49] px-8 text-lg font-bold text-[#14213d] hover:bg-[#fcbf49]/90" onClick={start}>
          Start a new life
        </Button>
        {localStorage.getItem(SAVE_KEY) && (
          <Button variant="outline" className="border-slate-500 text-slate-200 hover:bg-white/10" onClick={() => { try { setLife(JSON.parse(localStorage.getItem(SAVE_KEY)!)) } catch { /* ignore */ } }}>
            Continue saved life
          </Button>
        )}
      </div>
    )
  }

  const tabs: [Tab, string][] = [['life', 'Life'], ['activities', 'Activities'], ['career', 'Career'], ['relationships', 'People']]

  return (
    <div className="mx-auto flex min-h-dvh max-w-md flex-col bg-[#f1f3f5]">
      <header className="bg-[#14213d] px-4 pb-3 pt-4 text-white">
        <div className="flex items-baseline justify-between gap-2">
          <h1 className="truncate text-lg font-bold">{life.name}</h1>
          <span className="shrink-0 text-sm text-slate-300">Age {life.age}</span>
        </div>
        <div className="mt-0.5 flex items-baseline justify-between gap-2 text-xs text-slate-400">
          <span>{life.country}{life.career ? ` · ${life.career.track[life.career.level]}` : life.universityYears > 0 ? ' · University student' : life.student ? ' · Student' : ''}</span>
          <span className="shrink-0 font-semibold text-[#fcbf49]">{fmtMoney(life.money)}</span>
        </div>
        <div className="mt-3 grid grid-cols-1 gap-1.5 rounded-lg bg-white/95 p-3">
          <StatBar label="Happiness" value={life.happiness} color="bg-emerald-500" />
          <StatBar label="Health" value={life.health} color="bg-rose-500" />
          <StatBar label="Smarts" value={life.smarts} color="bg-sky-500" />
          <StatBar label="Looks" value={life.looks} color="bg-amber-400" />
        </div>
      </header>

      {life.dead ? (
        <div className="flex flex-1 flex-col items-center justify-center gap-4 px-8 text-center">
          <h2 className="text-2xl font-bold text-slate-800">{life.name} has died</h2>
          <p className="text-slate-600">Age {life.age} · {life.deathCause}</p>
          <p className="text-sm text-slate-500">
            Net worth {fmtMoney(life.money)} · {life.career ? `worked as ${life.career.title}` : 'never employed'} · {life.people.filter((p) => p.kind === 'friend').length} friends
          </p>
          <Button className="bg-[#14213d]" onClick={start}>Start a new life</Button>
        </div>
      ) : (
        <>
          <main className="flex min-h-0 flex-1 flex-col">
            {tab === 'life' && <LifeLog log={life.log} />}
            {tab === 'activities' && <ActivitiesPanel life={life} onActivity={onActivity} />}
            {tab === 'career' && <CareerPanel life={life} data={data} onApply={(row) => setLife(applyForJob(life, row))} onQuit={() => setLife(quitJob(life))} />}
            {tab === 'relationships' && <RelationshipsPanel life={life} onSpendTime={(id) => setLife(spendTime(life, id))} />}
          </main>

          <div className="flex justify-center bg-transparent pb-1">
            <button
              aria-label="Age up"
              onClick={onAgeUp}
              disabled={!!scenario}
              className="flex h-16 w-16 cursor-pointer items-center justify-center rounded-full bg-[#fcbf49] text-4xl font-black text-[#14213d] shadow-lg transition-transform duration-150 hover:scale-105 active:scale-95 disabled:opacity-50"
            >
              +
            </button>
          </div>
        </>
      )}

      <nav className="grid grid-cols-4 border-t border-slate-200 bg-white pb-[env(safe-area-inset-bottom)]">
        {tabs.map(([key, label]) => (
          <button
            key={key}
            onClick={() => setTab(key)}
            className={`h-12 cursor-pointer text-sm font-medium transition-colors ${tab === key ? 'text-[#14213d] underline decoration-[#fcbf49] decoration-2 underline-offset-4' : 'text-slate-400 hover:text-slate-600'}`}
          >
            {label}
          </button>
        ))}
      </nav>

      <ScenarioDialog scenario={scenario} onChoose={onChoose} />
    </div>
  )
}
