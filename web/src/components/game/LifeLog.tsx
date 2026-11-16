import { useEffect, useRef } from 'react'
import type { LogEntry } from '@/game/types'

const KIND_STYLES: Record<LogEntry['kind'], string> = {
  life: 'text-slate-700',
  scenario: 'text-slate-700',
  choice: 'text-slate-900 font-medium',
  news: 'text-sky-800 italic',
  money: 'text-emerald-800',
  death: 'text-rose-800 font-semibold',
}

export default function LifeLog({ log }: { log: LogEntry[] }) {
  const ref = useRef<HTMLDivElement>(null)
  useEffect(() => {
    ref.current?.scrollTo({ top: 0 })
  }, [log.length])
  return (
    <div ref={ref} className="flex-1 space-y-3 overflow-y-auto px-4 py-4">
      {log.map((e) => (
        <div key={e.id} className="flex gap-3 border-b border-slate-100 pb-3 text-sm leading-relaxed last:border-0">
          <span className="w-14 shrink-0 text-right text-xs font-semibold text-slate-400">{e.age === 0 ? 'Birth' : `Age ${e.age}`}</span>
          <p className={KIND_STYLES[e.kind]}>{e.text}</p>
        </div>
      ))}
    </div>
  )
}
