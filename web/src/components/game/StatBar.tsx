interface Props {
  label: string
  value: number
  color: string
}

export default function StatBar({ label, value, color }: Props) {
  return (
    <div className="flex items-center gap-2 text-xs">
      <span className="w-18 shrink-0 font-medium text-slate-600">{label}</span>
      <div className="h-2.5 flex-1 overflow-hidden rounded-full bg-slate-200">
        <div
          className={`h-full rounded-full transition-all duration-300 ${color}`}
          style={{ width: `${Math.max(0, Math.min(100, value))}%` }}
        />
      </div>
      <span className="w-8 text-right tabular-nums text-slate-500">{Math.round(value)}</span>
    </div>
  )
}
