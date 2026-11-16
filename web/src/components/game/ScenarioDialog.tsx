import { Dialog, DialogContent, DialogDescription, DialogHeader, DialogTitle } from '@/components/ui/dialog'
import { Button } from '@/components/ui/button'
import type { Scenario } from '@/game/types'

interface Props {
  scenario: Scenario | null
  onChoose: (choiceIndex: number) => void
}

export default function ScenarioDialog({ scenario, onChoose }: Props) {
  return (
    <Dialog open={!!scenario}>
      <DialogContent className="max-w-sm [&>button]:hidden" onInteractOutside={(e) => e.preventDefault()} onEscapeKeyDown={(e) => e.preventDefault()}>
        {scenario && (
          <>
            <DialogHeader>
              <DialogTitle className="text-xl">{scenario.title}</DialogTitle>
              <DialogDescription className="sr-only">{scenario.cta}</DialogDescription>
            </DialogHeader>
            <p className="text-base leading-relaxed text-slate-700">{scenario.body}</p>
            <p className="font-semibold text-slate-900">{scenario.cta}</p>
            <div className="flex flex-col gap-2">
              {scenario.choices.map((c, i) => (
                <Button
                  key={i}
                  variant="outline"
                  className="h-auto min-h-11 w-full justify-start whitespace-normal px-4 py-3 text-left text-sm"
                  onClick={() => onChoose(i)}
                >
                  {c.label}
                </Button>
              ))}
            </div>
          </>
        )}
      </DialogContent>
    </Dialog>
  )
}
