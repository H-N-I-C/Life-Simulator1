export type Gender = 'male' | 'female'

export interface Person {
  id: string
  name: string
  gender: Gender
  age: number
  relationship: number // 0-100
  kind: 'mother' | 'father' | 'friend' | 'partner' | 'child'
}

export interface Career {
  title: string
  track: string[]
  level: number
  salary: number
  salaryCap: number
  performance: number // 0-100
  yearsInRole: number
  type: string
}

export interface LogEntry {
  id: number
  age: number
  text: string
  kind: 'life' | 'scenario' | 'choice' | 'news' | 'money' | 'death'
}

export interface LifeState {
  name: string
  gender: Gender
  country: string
  age: number
  money: number
  happiness: number
  health: number
  smarts: number
  looks: number
  karma: number
  discipline: number
  student: boolean
  education: 'None' | 'High School' | 'University'
  universityYears: number
  career: Career | null
  people: Person[]
  log: LogEntry[]
  dead: boolean
  deathCause?: string
}

export interface Choice {
  label: string
  narrative: string
  impacts: Record<string, number>
}

export interface Scenario {
  title: string
  body: string
  cta: string
  choices: Choice[]
  source: 'universal' | 'career' | 'friend' | 'alcohol'
  friendId?: string
}
