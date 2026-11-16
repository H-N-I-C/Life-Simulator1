// BitLife scenario text DSL:
//   <a/b/c>        random option
//   <num5-100>     random integer in range
//   <friend>, <friend-possessive>, <he/she>, <his/her>, <him/her>
//   <country>, <teacher>, <singular>, <plural>, <rb> (last random option picked)

export interface TemplateContext {
  friendName?: string
  friendGender?: 'male' | 'female'
  country?: string
  drinkSingular?: string
  drinkPlural?: string
}

export function pick<T>(arr: T[]): T {
  return arr[Math.floor(Math.random() * arr.length)]
}

export function renderTemplate(input: string, ctx: TemplateContext): string {
  let lastRandom = ''
  let out = input

  out = out.replace(/<num(\d+)-(\d+)>/g, (_m, a, b) => {
    const lo = Number(a)
    const hi = Number(b)
    return String(lo + Math.floor(Math.random() * (hi - lo + 1)))
  })

  const pronouns: Record<string, string> = {
    'he/she': ctx.friendGender === 'female' ? 'she' : 'he',
    'his/her': ctx.friendGender === 'female' ? 'her' : 'his',
    'him/her': ctx.friendGender === 'female' ? 'her' : 'him',
    friend: ctx.friendName ?? 'a friend',
    'friend-possessive': ctx.friendName ? `${ctx.friendName}'s` : "a friend's",
    country: ctx.country ?? 'the country',
    teacher: 'your teacher',
    singular: ctx.drinkSingular ?? 'a drink',
    plural: ctx.drinkPlural ?? 'drinks',
  }
  for (const [key, value] of Object.entries(pronouns)) {
    out = out.split(`<${key}>`).join(value)
  }

  out = out.replace(/<rb>/g, () => lastRandom || 'item')

  // generic random options <a/b/c> — skip named/number tokens already handled
  out = out.replace(/<([a-zA-Z0-9][^<>]*\/[^<>]*)>/g, (m, body: string) => {
    if (/^num\d+-\d+$/.test(body)) return m
    const options = body.split('/')
    if (options.some((o) => o.length > 60)) return m
    lastRandom = pick(options)
    return lastRandom
  })

  // any leftover unknown token: strip angle brackets gracefully
  out = out.replace(/<([^<>]{1,40})>/g, (_m, body: string) => body)
  return out
}
