import type { Career, Choice, Gender, LifeState, LogEntry, Person, Scenario } from './types'
import type { GameData } from './data'
import { pick, renderTemplate } from './templates'

// Placeholder name pools — the original name lists live in stripped
// MonoBehaviour payloads (see deobfuscation/README.md, open item 3).
const MALE_NAMES = ['James', 'Liam', 'Noah', 'Ethan', 'Lucas', 'Mason', 'Oliver', 'Elijah', 'Henry', 'Jack', 'Owen', 'Wyatt']
const FEMALE_NAMES = ['Olivia', 'Emma', 'Ava', 'Sophia', 'Isabella', 'Mia', 'Amelia', 'Harper', 'Ella', 'Grace', 'Chloe', 'Zoe']
const SURNAMES = ['Smith', 'Johnson', 'Brown', 'Garcia', 'Miller', 'Davis', 'Wilson', 'Moore', 'Taylor', 'Clark', 'Walker', 'Young']
const COUNTRIES = ['United States', 'Canada', 'United Kingdom', 'Australia', 'Germany', 'France', 'Spain', 'Brazil', 'Japan', 'India']

const rand = (lo: number, hi: number) => lo + Math.floor(Math.random() * (hi - lo + 1))
export const clamp = (n: number) => Math.max(0, Math.min(100, n))
const money = (s: string) => Number((s || '').replace(/[$,]/g, '')) || 0
export const fmtMoney = (n: number) =>
  (n < 0 ? '-' : '') + '$' + Math.abs(Math.round(n)).toLocaleString('en-US')

let logId = 1
function log(s: LifeState, text: string, kind: LogEntry['kind'] = 'life') {
  if (text.trim()) s.log.unshift({ id: logId++, age: s.age, text, kind })
}

export function personName(gender: Gender): string {
  return `${pick(gender === 'female' ? FEMALE_NAMES : MALE_NAMES)} ${pick(SURNAMES)}`
}

export function newLife(data: GameData): LifeState {
  const gender: Gender = Math.random() < 0.5 ? 'male' : 'female'
  const country = pick(COUNTRIES)
  const mother: Person = { id: 'mother', name: personName('female'), gender: 'female', age: rand(20, 40), relationship: rand(70, 100), kind: 'mother' }
  const father: Person = { id: 'father', name: personName('male'), gender: 'male', age: mother.age + rand(-3, 6), relationship: rand(70, 100), kind: 'father' }
  const s: LifeState = {
    name: personName(gender),
    gender,
    country,
    age: 0,
    money: 0,
    happiness: rand(60, 100),
    health: rand(60, 100),
    smarts: rand(20, 100),
    looks: rand(20, 100),
    karma: 50,
    discipline: 50,
    student: false,
    education: 'None',
    universityYears: 0,
    career: null,
    people: [mother, father],
    log: [],
    dead: false,
  }
  const birth = pick(data.births)
  log(s, `I was born a ${gender === 'female' ? 'baby girl' : 'baby boy'} in ${country}.`, 'life')
  log(s, `My name is ${s.name}. My mother is ${mother.name} (age ${mother.age}) and my father is ${father.name} (age ${father.age}).`, 'life')
  if (birth?.Circumstance) log(s, birth.Circumstance, 'life')
  return s
}

function applyImpacts(s: LifeState, impacts: Record<string, number>, friendId?: string) {
  if (impacts.happiness) s.happiness = clamp(s.happiness + impacts.happiness)
  if (impacts.health) s.health = clamp(s.health + impacts.health)
  if (impacts.intelligence) s.smarts = clamp(s.smarts + impacts.intelligence)
  if (impacts.appearance) s.looks = clamp(s.looks + impacts.appearance)
  if (impacts.karma) s.karma = clamp(s.karma + impacts.karma)
  if (impacts.discipline) s.discipline = clamp(s.discipline + impacts.discipline)
  if (impacts.job && s.career) s.career.performance = clamp(s.career.performance + impacts.job * 3)
  if (impacts.friend && friendId) {
    const p = s.people.find((x) => x.id === friendId)
    if (p) p.relationship = clamp(p.relationship + impacts.friend * 5)
  }
}

const IMPACT_COLUMNS: [string, keyof Record<string, number>][] = [
  ['Karma Impact', 'karma'],
  ['Discipline Impact', 'discipline'],
  ['Happiness Impact', 'happiness'],
  ['Intelligence Impact', 'intelligence'],
  ['Appearance Impact', 'appearance'],
  ['Job Impact', 'job'],
  ['Friend Impact', 'friend'],
]

function parseChoices(row: Record<string, string>, max = 4): Choice[] {
  const choices: Choice[] = []
  for (let i = 1; i <= max; i++) {
    const label = row[`Choice ${i}`]
    if (!label) continue
    const impacts: Record<string, number> = {}
    for (const [col, key] of IMPACT_COLUMNS) {
      const v = Number(row[`${col} ${i}`])
      if (v) impacts[key] = v
    }
    choices.push({ label, narrative: row[`Narrative ${i}`] || '', impacts })
  }
  return choices
}

function matches(row: Record<string, string>, s: LifeState): boolean {
  const minAge = Number(row['Hero Min Age'] || row['Min Age'] || 0)
  const maxAge = Number(row['Hero Max Age'] || row['Max Age'] || 200)
  if (s.age < minAge || s.age > maxAge) return false
  if (row['Student'] === 'x' && !s.student) return false
  if (row['Job'] === 'x' && !s.career) return false
  if (row['Vampire'] === 'x') return false // vampire expansion not ported
  const g = (row['Hero Gender'] || '').toLowerCase()
  if (g && g !== s.gender) return false
  if (row['Both Parents Alive'] === 'x' && s.people.filter((p) => p.kind === 'mother' || p.kind === 'father').length < 2) return false
  return true
}

function templateCtx(s: LifeState, data: GameData, friend?: Person) {
  const drink = pick(data.alcohol)
  return {
    friendName: friend?.name,
    friendGender: friend?.gender,
    country: s.country,
    drinkSingular: drink?.Singular,
    drinkPlural: drink?.Plural,
  }
}

export function scenarioFromRow(
  row: Record<string, string>,
  s: LifeState,
  data: GameData,
  source: Scenario['source'],
  friend?: Person,
): Scenario {
  const ctx = templateCtx(s, data, friend)
  return {
    title: row['Title'] || 'Life',
    body: renderTemplate(row['Body'] || row['Scenario'] || '', ctx),
    cta: row['CTA'] || 'What will you do?',
    choices: parseChoices(row).map((c) => ({ ...c, narrative: renderTemplate(c.narrative, ctx) })),
    source,
    friendId: friend?.id,
  }
}

export interface AgeUpResult {
  state: LifeState
  scenario: Scenario | null
}

export function ageUp(prev: LifeState, data: GameData): AgeUpResult {
  const s: LifeState = { ...prev, people: prev.people.map((p) => ({ ...p })), career: prev.career ? { ...prev.career } : null, log: [...prev.log] }
  s.age += 1
  s.people.forEach((p) => (p.age += 1))

  // life stage transitions
  if (s.age === 5) { s.student = true; log(s, 'I started elementary school.', 'life') }
  if (s.age === 13) log(s, 'I started middle school.', 'life')
  if (s.age === 15) log(s, 'I started high school.', 'life')
  if (s.age === 18) {
    s.student = false
    s.education = 'High School'
    log(s, 'I graduated from high school.', 'life')
  }
  if (s.universityYears > 0) {
    s.universityYears -= 1
    if (s.universityYears === 0) { s.education = 'University'; s.student = false; log(s, 'I graduated from university!', 'life') }
  }

  // career economics
  if (s.career) {
    const c = s.career
    s.money += c.salary
    log(s, `I earned ${fmtMoney(c.salary)} working as ${c.track[c.level] || c.title}.`, 'money')
    c.yearsInRole += 1
    c.performance = clamp(c.performance + rand(-5, 5))
    if (c.yearsInRole >= 3 && c.performance >= 65 && c.level < c.track.length - 1) {
      c.level += 1
      c.yearsInRole = 0
      c.salary = Math.min(Math.round(c.salary * 1.25), c.salaryCap)
      log(s, `I got promoted to ${c.track[c.level]}! My salary is now ${fmtMoney(c.salary)}.`, 'money')
    } else if (c.performance >= 50) {
      c.salary = Math.min(Math.round(c.salary * 1.04), c.salaryCap)
    }
  }

  // natural stat drift & aging
  s.happiness = clamp(s.happiness + rand(-4, 4))
  s.smarts = clamp(s.smarts + (s.student ? rand(0, 3) : rand(-1, 1)))
  if (s.age > 45) s.health = clamp(s.health - rand(0, Math.floor((s.age - 40) / 12)))
  s.looks = clamp(s.looks + (s.age > 40 ? rand(-3, 1) : rand(-1, 2)))

  // death check
  const deathChance = s.age < 55 ? 0 : Math.pow((s.age - 50) / 60, 3)
  if (s.health <= 0 || Math.random() < deathChance) {
    s.dead = true
    s.deathCause = s.health <= 0 ? 'failing health' : 'old age'
    log(s, `I died at the age of ${s.age}.`, 'death')
    return { state: s, scenario: null }
  }

  // scenario selection
  let scenario: Scenario | null = null
  const roll = Math.random()
  const friends = s.people.filter((p) => p.kind === 'friend')
  if (s.career && roll < 0.3) {
    const rows = data.careerScenarios.filter((r) => r['Type'] === s.career!.title)
    if (rows.length) scenario = scenarioFromRow(pick(rows), s, data, 'career')
  }
  if (!scenario && friends.length && roll < 0.5) {
    const friend = pick(friends)
    const rows = data.friendScenarios.filter((r) => {
      if (!matches(r, s)) return false
      const fMin = Number(r['Friend Min Age'] || 0)
      const fMax = Number(r['Friend Max Age'] || 200)
      return friend.age >= fMin && friend.age <= fMax
    })
    if (rows.length) scenario = scenarioFromRow(pick(rows), s, data, 'friend', friend)
  }
  if (!scenario && roll < 0.85) {
    const rows = data.universal.filter((r) => matches(r, s))
    if (rows.length) scenario = scenarioFromRow(pick(rows), s, data, 'universal')
  }
  if (!scenario && Math.random() < 0.35) {
    const news = pick(data.news)
    log(s, renderTemplate(news.Scenario, templateCtx(s, data)), 'news')
  }
  return { state: s, scenario }
}

export function resolveChoice(s0: LifeState, scenario: Scenario, choice: Choice): LifeState {
  const s: LifeState = { ...s0, people: s0.people.map((p) => ({ ...p })), career: s0.career ? { ...s0.career } : null, log: [...s0.log] }
  applyImpacts(s, choice.impacts, scenario.friendId)
  if (choice.narrative) log(s, choice.narrative, 'choice')
  return s
}

// ---------- activities ----------

export function doActivity(prev: LifeState, data: GameData, activity: string): AgeUpResult {
  const s: LifeState = { ...prev, people: prev.people.map((p) => ({ ...p })), career: prev.career ? { ...prev.career } : null, log: [...prev.log] }
  switch (activity) {
    case 'drink': {
      const rows = data.alcoholScenarios.filter((r) => (r['Condition'] === 'Job') === !!s.career)
      const row = pick(rows)
      const drink = pick(data.alcohol)
      const ctx = templateCtx(s, data)
      return {
        state: s,
        scenario: {
          title: 'Nightlife',
          body: renderTemplate(row.Scenario, ctx),
          cta: row.CTA || 'What will you do?',
          choices: [
            { label: row['Yes Choice'] || 'Sure', narrative: `I had ${drink.Singular || 'a drink'}. It hit the spot.`, impacts: { happiness: 4, health: -2 } },
            { label: row['No Choice'] || 'No thanks', narrative: 'I turned down the drinks.', impacts: { health: 1 } },
          ],
          source: 'alcohol',
        },
      }
    }
    case 'friend': {
      const gender: Gender = Math.random() < 0.5 ? 'male' : 'female'
      const f: Person = { id: `friend-${Date.now()}`, name: personName(gender), gender, age: Math.max(4, s.age + rand(-3, 3)), relationship: rand(40, 80), kind: 'friend' }
      s.people.push(f)
      log(s, `I made a new friend: ${f.name}, age ${f.age}.`, 'life')
      s.happiness = clamp(s.happiness + 5)
      break
    }
    case 'gym': {
      s.health = clamp(s.health + rand(2, 6)); s.looks = clamp(s.looks + rand(1, 3))
      s.money -= s.age >= 18 ? 50 : 0
      log(s, 'I hit the gym. Feeling stronger.', 'life')
      break
    }
    case 'study': {
      s.smarts = clamp(s.smarts + rand(2, 6))
      log(s, 'I spent time studying. My mind feels sharper.', 'life')
      break
    }
    case 'meditate': {
      s.karma = clamp(s.karma + rand(1, 4)); s.happiness = clamp(s.happiness + rand(1, 5))
      log(s, 'I meditated and found some inner peace.', 'life')
      break
    }
    case 'doctor': {
      if (s.money < 200 && s.age >= 18) { log(s, 'I could not afford to see a doctor.', 'life'); break }
      if (s.age >= 18) s.money -= 200
      s.health = clamp(s.health + rand(5, 15))
      log(s, 'I saw a doctor. My health improved.', 'life')
      break
    }
    case 'university': {
      if (s.age >= 18 && s.education === 'High School' && s.universityYears === 0) {
        s.universityYears = 4; s.student = true
        log(s, 'I enrolled at university. Four years of study ahead.', 'life')
      }
      break
    }
    case 'spend-time': break // handled per-person in UI
  }
  return { state: s, scenario: null }
}

export function spendTime(prev: LifeState, personId: string): LifeState {
  const s: LifeState = { ...prev, people: prev.people.map((p) => ({ ...p })), log: [...prev.log] }
  const p = s.people.find((x) => x.id === personId)
  if (p) {
    p.relationship = clamp(p.relationship + rand(3, 10))
    s.happiness = clamp(s.happiness + 2)
    log(s, `I spent quality time with ${p.name}.`, 'life')
  }
  return s
}

// ---------- careers ----------

const EDUCATION_RANK: Record<string, number> = { None: 0, 'High School': 1, University: 2 }

export function eligibleCareers(s: LifeState, data: GameData): { row: Record<string, string>; ok: boolean; reason: string }[] {
  return data.careers
    .filter((r) => r['Unavailable'] !== 'x')
    .map((r) => {
      const minAge = Number(r['Min Age'] || 18)
      const reasons: string[] = []
      if (s.age < minAge) reasons.push(`age ${minAge}+`)
      const edu = r['Required Education'] || 'None'
      if ((EDUCATION_RANK[s.education] ?? 0) < (EDUCATION_RANK[edu] ?? 0)) reasons.push(edu)
      if (Number(r['Min Intelligence'] || 0) > s.smarts) reasons.push(`smarts ${r['Min Intelligence']}+`)
      if (Number(r['Min Appearance'] || 0) > s.looks) reasons.push(`looks ${r['Min Appearance']}+`)
      if (Number(r['Min Health'] || 0) > s.health) reasons.push(`health ${r['Min Health']}+`)
      const g = (r['Gender'] || '').toLowerCase()
      if (g && g !== s.gender) reasons.push(g)
      return { row: r, ok: reasons.length === 0, reason: reasons.join(', ') }
    })
    .sort((a, b) => Number(b.ok) - Number(a.ok))
}

export function applyForJob(prev: LifeState, row: Record<string, string>): LifeState {
  const s: LifeState = { ...prev, log: [...prev.log] }
  const track = (row['Seniority Track'] || row['Career']).split(',').map((t) => t.trim()).filter(Boolean)
  const career: Career = {
    title: row['Career'],
    track,
    level: 0,
    salary: money(row['Starting Salary']),
    salaryCap: money(row['Salary Cap']) || money(row['Starting Salary']),
    performance: 60,
    yearsInRole: 0,
    type: row['Career'],
  }
  s.career = career
  log(s, `I got hired as ${track[0] || career.title}! Starting salary: ${fmtMoney(career.salary)} a year.`, 'money')
  return s
}

export function quitJob(prev: LifeState): LifeState {
  const s: LifeState = { ...prev, log: [...prev.log], career: null }
  log(s, `I quit my job as ${prev.career?.track[prev.career.level]}.`, 'life')
  return s
}
