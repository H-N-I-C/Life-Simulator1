export interface GameData {
  universal: Record<string, string>[]
  careerScenarios: Record<string, string>[]
  friendScenarios: Record<string, string>[]
  careers: Record<string, string>[]
  births: Record<string, string>[]
  alcoholScenarios: Record<string, string>[]
  alcohol: Record<string, string>[]
  news: Record<string, string>[]
}

let cache: GameData | null = null

export async function loadGameData(): Promise<GameData> {
  if (cache) return cache
  const files: [keyof GameData, string][] = [
    ['universal', 'universal-scenarios.json'],
    ['careerScenarios', 'career-scenarios.json'],
    ['friendScenarios', 'friend-scenarios.json'],
    ['careers', 'careers.json'],
    ['births', 'birth-circumstances.json'],
    ['alcoholScenarios', 'alcohol-scenarios.json'],
    ['alcohol', 'alcohol.json'],
    ['news', 'news-events.json'],
  ]
  const entries = await Promise.all(
    files.map(async ([key, file]) => {
      const res = await fetch(`${import.meta.env.BASE_URL}data/${file}`)
      if (!res.ok) throw new Error(`Failed to load ${file}: ${res.status}`)
      return [key, await res.json()] as const
    }),
  )
  cache = Object.fromEntries(entries) as unknown as GameData
  return cache
}
