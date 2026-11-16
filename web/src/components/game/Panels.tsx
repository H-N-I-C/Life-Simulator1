import { Button } from '@/components/ui/button'
import type { GameData } from '@/game/data'
import { eligibleCareers, fmtMoney } from '@/game/engine'
import type { LifeState } from '@/game/types'

const btn = 'h-11 w-full justify-start px-4 text-sm'

export function ActivitiesPanel({ life, onActivity }: { life: LifeState; onActivity: (a: string) => void }) {
  return (
    <div className="flex-1 space-y-2 overflow-y-auto px-4 py-4">
      <h2 className="mb-2 text-lg font-bold text-slate-800">Activities</h2>
      <Button variant="outline" className={btn} onClick={() => onActivity('drink')} disabled={life.age < 13}>
        Go out for drinks {life.age < 13 && '(13+)'}
      </Button>
      <Button variant="outline" className={btn} onClick={() => onActivity('friend')} disabled={life.age < 4}>
        Make a new friend {life.age < 4 && '(4+)'}
      </Button>
      <Button variant="outline" className={btn} onClick={() => onActivity('gym')} disabled={life.age < 8}>
        Go to the gym {life.age < 8 && '(8+)'}
      </Button>
      <Button variant="outline" className={btn} onClick={() => onActivity('study')}>
        Study
      </Button>
      <Button variant="outline" className={btn} onClick={() => onActivity('meditate')}>
        Meditate
      </Button>
      <Button variant="outline" className={btn} onClick={() => onActivity('doctor')}>
        See a doctor {life.age >= 18 && '($200)'}
      </Button>
      {life.age >= 18 && life.education === 'High School' && life.universityYears === 0 && (
        <Button variant="outline" className={btn} onClick={() => onActivity('university')}>
          Enroll at university (4 years)
        </Button>
      )}
      {life.universityYears > 0 && (
        <p className="pt-2 text-sm text-slate-500">University: {4 - life.universityYears + 1 > 4 ? 4 : 4 - life.universityYears + 1} / 4 years completed</p>
      )}
    </div>
  )
}

export function CareerPanel({ life, data, onApply, onQuit }: { life: LifeState; data: GameData; onApply: (row: Record<string, string>) => void; onQuit: () => void }) {
  const careers = eligibleCareers(life, data)
  return (
    <div className="flex-1 overflow-y-auto px-4 py-4">
      <h2 className="mb-2 text-lg font-bold text-slate-800">Career</h2>
      {life.career ? (
        <div className="mb-4 rounded-lg bg-white p-4 shadow-sm">
          <p className="font-semibold text-slate-900">{life.career.track[life.career.level]}</p>
          <p className="text-sm text-slate-500">{life.career.title}</p>
          <div className="mt-3 grid grid-cols-2 gap-2 text-sm">
            <p className="text-slate-600">Salary: <span className="font-medium text-emerald-700">{fmtMoney(life.career.salary)}/yr</span></p>
            <p className="text-slate-600">Performance: <span className="font-medium">{life.career.performance}%</span></p>
          </div>
          <Button variant="outline" className="mt-4" onClick={onQuit}>Quit job</Button>
        </div>
      ) : (
        <p className="mb-4 text-sm text-slate-500">
          {life.age < 14 ? 'You are too young for a job. Enjoy childhood!' : 'No job. Apply below — requirements are checked against your age, education and stats.'}
        </p>
      )}
      <div className="space-y-2">
        {careers.map(({ row, ok, reason }) => (
          <div key={row.Career} className="flex items-center justify-between gap-2 rounded-lg bg-white p-3 shadow-sm">
            <div className="min-w-0">
              <p className="truncate text-sm font-semibold text-slate-800">{row.Career}</p>
              <p className="text-xs text-slate-500">
                {row['Company Type']} · {row['Starting Salary']}/yr
                {!ok && reason && <span className="text-rose-600"> · needs {reason}</span>}
              </p>
            </div>
            <Button size="sm" variant={ok ? 'default' : 'outline'} disabled={!ok || !!life.career || life.age < 14} onClick={() => onApply(row)}>
              Apply
            </Button>
          </div>
        ))}
      </div>
    </div>
  )
}

export function RelationshipsPanel({ life, onSpendTime }: { life: LifeState; onSpendTime: (id: string) => void }) {
  return (
    <div className="flex-1 space-y-2 overflow-y-auto px-4 py-4">
      <h2 className="mb-2 text-lg font-bold text-slate-800">Relationships</h2>
      {life.people.map((p) => (
        <div key={p.id} className="rounded-lg bg-white p-3 shadow-sm">
          <div className="flex items-center justify-between">
            <div>
              <p className="text-sm font-semibold text-slate-800">{p.name}</p>
              <p className="text-xs capitalize text-slate-500">{p.kind} · age {p.age}</p>
            </div>
            <Button size="sm" variant="outline" onClick={() => onSpendTime(p.id)}>Spend time</Button>
          </div>
          <div className="mt-2 h-2 overflow-hidden rounded-full bg-slate-200">
            <div className="h-full rounded-full bg-pink-400 transition-all duration-300" style={{ width: `${p.relationship}%` }} />
          </div>
        </div>
      ))}
    </div>
  )
}
