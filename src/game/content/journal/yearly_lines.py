"""Yearly journal flavour lines (Kimi C, redesign-v2 F4).

Original, first-person, age-banded lines that replace the generic
"X turned N." year entry with something that reads like a life. Two parts:

  LINES          the content pool (data below — id, band, text, when)
  pick_year_line selection: condition match, no repeats against `recent`,
                 weighted-random via the caller's rng

The engine hook (A, contracts.md §Journal flavour) is `year_journal_line`:
it assembles the observer-safe context from env, draws from the COSMETIC
rng stream (never the simulation stream), and persists the recents list in
env["journal_line_recents"].

Condition dialect (all observer-safe, player-known only):
  when: {"all": [cond...], "any": [cond...]}   (all = AND, any = OR)
  cond:
    {"stat": "happiness|health|smarts|looks", "min": int, "max": int}
    {"flag": "employed|unemployed|partner|children|mother|father|"
             "ill|dropped_out|retired", "is": bool}
    {"school": "elementary|middle|high|university|none"}
    {"role_relation": "mother|father|partner|sibling|child|any",
     "min": int, "max": int}
"""

from src.lifesim_lib.lifesim_lib import Gender

# ---------------------------------------------------------------------------
# Age bands
# ---------------------------------------------------------------------------

BANDS = {
    "baby": (0, 1),
    "toddler": (2, 4),
    "child": (5, 12),
    "teen": (13, 17),
    "adult": (18, 64),
    "elder": (65, 150),
}


def band_for_age(age):
    for name, (lo, hi) in BANDS.items():
        if lo <= age <= hi:
            return name
    return None


# ---------------------------------------------------------------------------
# Line pool. Every line is original, first person, past tense, short.
# ---------------------------------------------------------------------------

LINES = [
    # ------------------------------------------------------------- baby 0-1
    {"id": "baby_mobile_1", "band": "baby",
     "text": "I discovered that the mobile above my crib spins faster the "
             "harder I kick."},
    {"id": "baby_toes_1", "band": "baby",
     "text": "I found my toes today. They are excellent and I intend to "
             "keep them."},
    {"id": "baby_spoon_1", "band": "baby",
     "text": "I threw my spoon on the floor eleven times. It kept coming "
             "back. Fascinating."},
    {"id": "baby_words_1", "band": "baby",
     "text": "I said my first word and the whole kitchen erupted like I had "
             "announced a royal decree."},
    {"id": "baby_mirror_1", "band": "baby",
     "text": "There is another baby in the mirror. We have an arrangement: "
             "I smile, they smile."},
    {"id": "baby_steps_1", "band": "baby",
     "text": "I took three wobbling steps and grabbed the table leg like a "
             "conquering hero."},
    {"id": "baby_napwar_1", "band": "baby",
     "text": "I lost the battle against my afternoon nap, but I made them "
             "work for it."},
    {"id": "baby_peekaboo_1", "band": "baby",
     "text": "Peekaboo has stopped fooling me. I know exactly where they "
             "went. I choose to clap anyway."},
    {"id": "baby_laugh_1", "band": "baby",
     "text": "The dog sneezed and I laughed so hard I fell sideways.",
     "when": {"all": [{"stat": "happiness", "min": 60}]}},
    {"id": "baby_teeth_1", "band": "baby",
     "text": "A tooth is coming in and I have decided to lodge a formal "
             "complaint about it.",
     "when": {"all": [{"stat": "happiness", "max": 45}]}},
    {"id": "baby_cold_1", "band": "baby",
     "text": "A cold kept me sniffly for days. I napped through most of "
             "the scandal.",
     "when": {"all": [{"flag": "ill", "is": True}]}},
    {"id": "baby_cuddle_1", "band": "baby",
     "text": "I fell asleep mid-cuddle and woke up in my crib, carried "
             "there like a sack of warm potatoes."},
    {"id": "baby_bath_1", "band": "baby",
     "text": "Splashing is now my primary profession. Everything else is "
             "a hobby."},
    {"id": "baby_solid_1", "band": "baby",
     "text": "They tried to feed me mashed peas. I reviewed them "
             "poorly."},
    {"id": "baby_giggle_1", "band": "baby",
     "text": "I learned that my own giggle makes adults do funny things. I "
             "deployed it all day.",
     "when": {"all": [{"stat": "happiness", "min": 70}]}},
    {"id": "baby_blanket_1", "band": "baby",
     "text": "My blanket and I reached an understanding: it goes where I "
             "go."},

    # ---------------------------------------------------------- toddler 2-4
    {"id": "toddler_why_1", "band": "toddler",
     "text": "I asked why. Then why again. Then why nine more times, just "
             "to watch the grown-ups think."},
    {"id": "toddler_shoes_1", "band": "toddler",
     "text": "I put my shoes on by myself. Both on the wrong feet. I "
             "accept no criticism."},
    {"id": "toddler_drawing_1", "band": "toddler",
     "text": "I drew our family. Everyone is a circle with sticks. It is "
             "my masterpiece."},
    {"id": "toddler_puddle_1", "band": "toddler",
     "text": "There was a puddle and there were boots. Some things are "
             "simply meant to be."},
    {"id": "toddler_petty_1", "band": "toddler",
     "text": "I was offered two identical biscuits and chose the one on "
             "the left. The right one was apparently ruined by this."},
    {"id": "toddler_stairs_1", "band": "toddler",
     "text": "I counted the stairs all by myself and only missed one."},
    {"id": "toddler_shadow_1", "band": "toddler",
     "text": "My shadow keeps following me. Running doesn't help. It is "
             "very committed."},
    {"id": "toddler_cup_1", "band": "toddler",
     "text": "I drank from a big-kid cup and only wore half of it."},
    {"id": "toddler_story_1", "band": "toddler",
     "text": "I demanded the same bedtime story four times, then corrected "
             "the teller on a line."},
    {"id": "toddler_blocks_1", "band": "toddler",
     "text": "I built a tower taller than me. Then I destroyed it, as is "
             "tradition."},
    {"id": "toddler_noclothes_1", "band": "toddler",
     "text": "I have decided clothes are optional. The grown-ups remain "
             "unconvinced."},
    {"id": "toddler_song_1", "band": "toddler",
     "text": "I invented a song. It has one word and it is my name."},
    {"id": "toddler_swing_1", "band": "toddler",
     "text": "I went higher on the swing than ever before. My stomach "
             "stayed at the bottom but I didn't mind."},
    {"id": "toddler_hug_1", "band": "toddler",
     "text": "I gave someone a hug with my whole body, arms, legs and "
             "everything. They seemed better afterwards.",
     "when": {"all": [{"stat": "happiness", "min": 65}]}},
    {"id": "toddler_fever_1", "band": "toddler",
     "text": "A fever kept me on the couch with juice. I milked it for "
             "three extra stories.",
     "when": {"all": [{"flag": "ill", "is": True}]}},
    {"id": "toddler_broccoli_1", "band": "toddler",
     "text": "I hid a broccoli under my plate. The plan had one flaw: the "
             "plate was transparent."},
    {"id": "toddler_sibling_1", "band": "toddler",
     "text": "There is a baby in the house and I am observing it "
             "closely. It is loud and has no hobbies.",
     "when": {"all": [{"flag": "children", "is": True}]}},
    {"id": "toddler_momsoft_1", "band": "toddler",
     "text": "Mom read to me until I fell asleep on her shoulder. Best "
             "deal I ever made.",
     "when": {"all": [{"flag": "mother", "is": True},
                      {"role_relation": "mother", "min": 70}]}},
    {"id": "toddler_dadairport_1", "band": "toddler",
     "text": "Dad turned himself into an aeroplane and I was the entire "
             "passenger list.",
     "when": {"all": [{"flag": "father", "is": True}]}},
    {"id": "toddler_wordno_1", "band": "toddler",
     "text": "I learned the word no. It has become the backbone of my "
             "whole philosophy.",
     "when": {"all": [{"stat": "happiness", "max": 50}]}},
    {"id": "toddler_imagine_1", "band": "toddler",
     "text": "The floor is lava. The couch is a boat. The dog is a sea "
             "monster. Everyone knows this."},
    {"id": "toddler_icecream_1", "band": "toddler",
     "text": "I tasted ice cream for the first time and briefly forgot "
             "every other food exists."},
    {"id": "toddler_bedtime_1", "band": "toddler",
     "text": "Bedtime was announced. I countered with one more song, one "
             "more story and three questions about the moon. It worked "
             "twice."},
    {"id": "toddler_fall_1", "band": "toddler",
     "text": "I scraped my knee racing the dog. I accepted medical "
             "attention in the form of a cartoon bandage.",
     "when": {"all": [{"stat": "health", "max": 55}]}},
    {"id": "toddler_words_1", "band": "toddler",
     "text": "I learned the word tomorrow and it has made waiting "
             "impossible."},
    {"id": "toddler_umbrella_1", "band": "toddler",
     "text": "I got a tiny umbrella and now I check the sky hourly for "
             "work."},

    # ------------------------------------------------------------ child 5-12
    {"id": "child_losttooth_1", "band": "child",
     "text": "A tooth fell out and something left a coin under my pillow. "
             "I have questions and a business idea."},
    {"id": "child_recess_1", "band": "child",
     "text": "At recess I ran so fast my shoes almost came off. It "
             "counted as winning."},
    {"id": "child_desk_1", "band": "child",
     "text": "I got moved to the front desk for talking. Now I am a "
             "front-desk talker."},
    {"id": "child_homework_1", "band": "child",
     "text": "I did my homework in record time, then found out it was the "
             "wrong page."},
    {"id": "child_rain_1", "band": "child",
     "text": "It rained all Saturday and I built a fort out of every "
             "blanket we own."},
    {"id": "child_bike_1", "band": "child",
     "text": "I rode my bike with no hands for two whole seconds. I have "
             "not stopped telling people."},
    {"id": "child_snow_1", "band": "child",
     "text": "The first snow cancelled nothing, so I cancelled it myself "
             "and went sledding anyway."},
    {"id": "child_bully_1", "band": "child",
     "text": "A bigger kid tried to take my lunch. We had words. My words "
             "had backup.",
     "when": {"all": [{"stat": "happiness", "max": 55}]}},
    {"id": "child_report_1", "band": "child",
     "text": "My report card came home and the fridge is now displaying "
             "it like art.",
     "when": {"all": [{"school": "elementary"}, {"stat": "smarts", "min": 70}]}},
    {"id": "child_test_1", "band": "child",
     "text": "I studied harder than ever for the big test and it "
             "actually showed.",
     "when": {"all": [{"school": "elementary"}, {"stat": "smarts", "min": 75}]}},
    {"id": "child_dreamjob_1", "band": "child",
     "text": "This week I will be an astronaut, a vet and a dragon. Next "
             "week, probably a chef."},
    {"id": "child_pet_1", "band": "child",
     "text": "I asked for a pet for the three hundredth day running. "
             "Persistence is a virtue."},
    {"id": "child_friend_1", "band": "child",
     "text": "I made a new friend over a shared orange and a dispute "
             "about dinosaurs. We're inseparable now."},
    {"id": "child_library_1", "band": "child",
     "text": "I found a book so good I read it under the blanket with a "
             "torch.",
     "when": {"all": [{"stat": "smarts", "min": 60}]}},
    {"id": "child_science_1", "band": "child",
     "text": "My science experiment grew something unplanned. We are "
             "calling it a discovery.",
     "when": {"all": [{"stat": "smarts", "min": 65}]}},
    {"id": "child_soccer_1", "band": "child",
     "text": "I scored the tying goal with my shoulder. Technique is "
             "optional at this level.",
     "when": {"all": [{"stat": "health", "min": 65}]}},
    {"id": "child_sick_1", "band": "child",
     "text": "I caught something at school and spent two days in bed "
             "being dramatically unwell.",
     "when": {"all": [{"flag": "ill", "is": True}]}},
    {"id": "child_pocketmoney_1", "band": "child",
     "text": "I saved my pocket money for a month and bought the thing. "
             "The thing is already boring, but I own it."},
    {"id": "child_birthday_1", "band": "child",
     "text": "My birthday cake had the right number of candles. I checked "
             "twice and blew them all out in one go."},
    {"id": "child_movingup_1", "band": "child",
     "text": "I moved up a reading level and the librarian gave me a "
             "whole new shelf to conquer."},
    {"id": "child_camp_1", "band": "child",
     "text": "At camp I canoed, capsized, and came back with the best "
             "story of the summer."},
    {"id": "child_argument_1", "band": "child",
     "text": "My friend and I argued about whose dad is taller. Our "
             "friendship survived the scandal."},
    {"id": "child_grandma_1", "band": "child",
     "text": "I visited my grandparents and came home with cookies, a "
             "coin and absolutely no regrets."},
    {"id": "child_heels_1", "band": "child",
     "text": "I tried walking in my mother's heels and respect her "
             "daily commute enormously now."},
    {"id": "child_club_1", "band": "child",
     "text": "I started a secret club. Membership is me and two friends. "
             "Dues are snacks."},
    {"id": "child_concert_1", "band": "child",
     "text": "I played in the school concert. Two wrong notes, one bow "
             "too low, zero regrets."},
    {"id": "child_spelling_1", "band": "child",
     "text": "I won the class spelling bee on a word I learned by "
             "accident.",
     "when": {"all": [{"stat": "smarts", "min": 72}]}},
    {"id": "child_grumpy_1", "band": "child",
     "text": "I was in a mood about nothing in particular and it "
             "followed me around all day.",
     "when": {"all": [{"stat": "happiness", "max": 40}]}},
    {"id": "child_summer_1", "band": "child",
     "text": "Summer holidays started and the days suddenly have no "
             "edges. Glorious."},
    {"id": "child_teacher_1", "band": "child",
     "text": "My teacher said my essay made her laugh out loud. I have "
             "decided to become insufferable about it.",
     "when": {"all": [{"school": "elementary"}, {"stat": "smarts", "min": 68}]}},
    {"id": "child_glasses_1", "band": "child",
     "text": "I got glasses and the world suddenly has sharp edges. Trees "
             "are a revelation."},
    {"id": "child_team_1", "band": "child",
     "text": "I joined the team and mostly warm the bench, but I warm it "
             "with total commitment."},
    {"id": "child_recipe_1", "band": "child",
     "text": "I cooked dinner with only minor structural damage to the "
             "kitchen. Everyone said it was good. It was not bad."},
    {"id": "child_storm_1", "band": "child",
     "text": "A storm knocked the power out and we told stories by "
             "torchlight until it came back."},
    {"id": "child_garden_1", "band": "child",
     "text": "I planted seeds in the garden and check them every morning "
             "like a tiny worried farmer."},
    {"id": "child_haircut_1", "band": "child",
     "text": "I gave myself a haircut with craft scissors. The "
             "professional who fixed it was very kind about it."},
    {"id": "child_honest_1", "band": "child",
     "text": "I told a relative their new perfume smelled like soup. I "
             "am told honesty is a virtue."},
    {"id": "child_magnet_1", "band": "child",
     "text": "I learned about magnets and have been stuck to the fridge "
             "metaphorically ever since."},
    {"id": "child_swim_1", "band": "child",
     "text": "I swam a whole length underwater and came up to applause "
             "from exactly one lifeguard."},
    {"id": "child_babysit_1", "band": "child",
     "text": "I earned a coin babysitting my cousin. He is two and "
             "out-negotiated me.",
     "when": {"all": [{"stat": "smarts", "min": 55}]}},
    {"id": "child_middle_1", "band": "child",
     "text": "Middle school hallways are a maze with lockers. I "
             "found my class on the third day, and I stand by the "
             "achievement.",
     "when": {"all": [{"school": "middle"}]}},
    {"id": "child_lockers_1", "band": "child",
     "text": "I mastered my locker combination and celebrated like it was "
             "a bank vault.",
     "when": {"all": [{"school": "middle"}]}},

    # -------------------------------------------------------------- teen 13-17
    {"id": "teen_alarm_1", "band": "teen",
     "text": "My alarm and I are in an abusive relationship. It screams, "
             "I hit snooze."},
    {"id": "teen_crush_1", "band": "teen",
     "text": "I talked to my crush and said the word absolutely at the "
             "wrong time. Replay available nightly."},
    {"id": "teen_exam_1", "band": "teen",
     "text": "Exams are over and I feel like a squeezed sponge. A free, "
             "sunny sponge.",
     "when": {"all": [{"school": "high"}]}},
    {"id": "teen_grades_up_1", "band": "teen",
     "text": "My grades climbed this term. I pretended it was effortless. "
             "It was not effortless.",
     "when": {"all": [{"school": "high"}, {"stat": "smarts", "min": 70}]}},
    {"id": "teen_grades_down_1", "band": "teen",
     "text": "My grades slipped and a very serious meeting was held. "
             "Attendees: everyone. Agenda: me.",
     "when": {"all": [{"school": "high"}, {"stat": "smarts", "max": 55}]}},
    {"id": "teen_license_1", "band": "teen",
     "text": "I passed my driving test on the second try. The first "
             "try is a story I tell at my own expense."},
    {"id": "teen_acne_1", "band": "teen",
     "text": "My skin declared war this week. I responded with pharmacy "
             "products and dignity.",
     "when": {"all": [{"stat": "looks", "max": 50}]}},
    {"id": "teen_parttime_1", "band": "teen",
     "text": "I have a part-time job now. The pay is small but it is "
             "entirely MINE.",
     "when": {"all": [{"flag": "employed", "is": True}]}},
    {"id": "teen_music_1", "band": "teen",
     "text": "I found a band that understands me. Nobody else is allowed "
             "to like them, apparently."},
    {"id": "teen_friendfall_1", "band": "teen",
     "text": "My best friend and I had a real fight. Three days of "
             "silence, then one joke, and it was over.",
     "when": {"all": [{"stat": "happiness", "max": 60}]}},
    {"id": "teen_mirror_1", "band": "teen",
     "text": "I caught my reflection and decided the hair situation is "
             "improving. Growth, literally."},
    {"id": "teen_prom_1", "band": "teen",
     "text": "There is apparently a dance and I have apparently been "
             "asked about my intentions regarding it."},
    {"id": "teen_sport_1", "band": "teen",
     "text": "I made the team this year. My jersey number is my whole "
             "personality now.",
     "when": {"all": [{"stat": "health", "min": 65}]}},
    {"id": "teen_phone_1", "band": "teen",
     "text": "My phone died at noon and I lived like a pioneer until "
             "dinner."},
    {"id": "teen_essay_1", "band": "teen",
     "text": "I wrote an essay I actually believe in and got marked down "
             "for being 'opinionated'. Noted.",
     "when": {"all": [{"stat": "smarts", "min": 65}]}},
    {"id": "teen_identity_1", "band": "teen",
     "text": "Someone asked what I want to be. Rude question. I have "
             "three answers and none of them match."},
    {"id": "teen_firstdate_1", "band": "teen",
     "text": "I went on a first date and spilt a drink within four "
             "minutes. They laughed, so I count it as a win."},
    {"id": "teen_mall_1", "band": "teen",
     "text": "We walked around the shops for three hours and bought "
             "nothing. Perfect day."},
    {"id": "teen_gym_1", "band": "teen",
     "text": "I started training properly and discovered muscles I did "
             "not know could hurt.",
     "when": {"all": [{"stat": "health", "min": 60}]}},
    {"id": "teen_sick_1", "band": "teen",
     "text": "I was ill for a week and missed two tests. The make-up "
             "work is my villain origin story.",
     "when": {"all": [{"flag": "ill", "is": True}]}},
    {"id": "teen_parenttalk_1", "band": "teen",
     "text": "We had the future talk at dinner. I answered every "
             "question with a shrug of varying sizes."},
    {"id": "teen_library2_1", "band": "teen",
     "text": "I have started reading for fun again. Somewhere, my "
             "younger self is very smug.",
     "when": {"all": [{"stat": "smarts", "min": 68}]}},
    {"id": "teen_volunteer_1", "band": "teen",
     "text": "I volunteered at the community centre and it beat every "
             "plan I had cancelled to do it."},
    {"id": "teen_sleepover_1", "band": "teen",
     "text": "At the sleepover we stayed up until three solving problems "
             "nobody will remember having."},
    {"id": "teen_math_1", "band": "teen",
     "text": "I finally beat the maths exam that had my number. Turns "
             "out the exam's number was mine.",
     "when": {"all": [{"school": "high"}, {"stat": "smarts", "min": 72}]}},
    {"id": "teen_heartbreak_1", "band": "teen",
     "text": "It ended and I wrote the whole thing into a notebook like "
             "a crime report. Case closed. Mostly.",
     "when": {"all": [{"stat": "happiness", "max": 45}]}},
    {"id": "teen_babysit2_1", "band": "teen",
     "text": "I babysat for real money and the kids went to bed on "
             "schedule. Professional at last."},
    {"id": "teen_graduation_1", "band": "teen",
     "text": "Graduation is apparently on the horizon and the adults "
             "keep saying how fast it goes. It did not go fast. It "
             "went exactly at the speed of homework.",
     "when": {"all": [{"school": "high"}]}},
    {"id": "teen_roadtrip_1", "band": "teen",
     "text": "We drove out of town just to see somewhere new and argued "
             "about the playlist the entire way."},
    {"id": "teen_motherfriend_1", "band": "teen",
     "text": "My mother asked about my day and actually listened to the "
             "whole answer. We are basically colleagues now.",
     "when": {"all": [{"flag": "mother", "is": True},
                      {"role_relation": "mother", "min": 65}]}},
    {"id": "teen_application_1", "band": "teen",
     "text": "I sent off applications and now check the mailbox with the "
             "energy of a detective.",
     "when": {"all": [{"school": "high"}, {"stat": "smarts", "min": 60}]}},
    {"id": "teen_badhabit_1", "band": "teen",
     "text": "I quit the thing I kept saying I'd quit. Day count: small "
             "but nonzero."},

    # ------------------------------------------------------------- adult 18-64
    {"id": "adult_rent_1", "band": "adult",
     "text": "I paid rent and watched my account deflate like a slow "
             "puncture."},
    {"id": "adult_cooking_1", "band": "adult",
     "text": "I cooked a meal with actual vegetables and only set off "
             "the smoke detector once."},
    {"id": "adult_jobgood_1", "band": "adult",
     "text": "Work went well this year and my boss used my name and the "
             "word impressed in the same sentence.",
     "when": {"all": [{"flag": "employed", "is": True}]}},
    {"id": "adult_jobhunt_1", "band": "adult",
     "text": "I am job hunting, which is a full-time job that pays "
             "nothing and rejects you by email.",
     "when": {"all": [{"flag": "unemployed", "is": True}]}},
    {"id": "adult_promotion_1", "band": "adult",
     "text": "I caught myself thinking about a bigger role at work. "
             "Ambition apparently does not respect my lunch break.",
     "when": {"all": [{"flag": "employed", "is": True}]}},
    {"id": "adult_date_1", "band": "adult",
     "text": "I went on a date that lasted four hours and felt like "
             "twenty minutes. Scheduling a second one is now a military "
             "operation."},
    {"id": "adult_movedin_1", "band": "adult",
     "text": "I keep a spare toothbrush at my partner's place. Neither "
             "of us has mentioned it. The toothbrush mentions it "
             "constantly.",
     "when": {"all": [{"flag": "partner", "is": True}]}},
    {"id": "adult_married_1", "band": "adult",
     "text": "My partner and I have started having the wedding "
             "conversation. It is mostly Pinterest boards and diplomatic "
             "pauses.",
     "when": {"all": [{"flag": "partner", "is": True}]}},
    {"id": "adult_baby_1", "band": "adult",
     "text": "My kid kept us up half the night and I am told I agreed "
             "to this deal years ago. The terms, apparently, were "
             "exactly this.",
     "when": {"all": [{"flag": "children", "is": True}]}},
    {"id": "adult_kidquote_1", "band": "adult",
     "text": "My kid asked why the sky doesn't fall. I gave a whole "
             "lecture. The follow-up question defeated me.",
     "when": {"all": [{"flag": "children", "is": True}]}},
    {"id": "adult_kidrecital_1", "band": "adult",
     "text": "I watched my child perform and felt my heart climb right "
             "out of my chest to applaud.",
     "when": {"all": [{"flag": "children", "is": True},
                      {"stat": "happiness", "min": 60}]}},
    {"id": "adult_gymadult_1", "band": "adult",
     "text": "I joined a gym in January like a folkloric creature and, "
             "against legend, I still go.",
     "when": {"all": [{"stat": "health", "min": 60}]}},
    {"id": "adult_checkup_1", "band": "adult",
     "text": "My check-up came with a lecture about my chair posture. My "
             "chair heard everything.",
     "when": {"all": [{"flag": "ill", "is": True}]}},
    {"id": "adult_coffee_1", "band": "adult",
     "text": "The barista at my usual place knows my order. I have "
             "become a regular at something. Growth."},
    {"id": "adult_flatpack_1", "band": "adult",
     "text": "I assembled flat-pack furniture and there were exactly "
             "zero screws left over. I tell everyone."},
    {"id": "adult_raise_1", "band": "adult",
     "text": "I rehearsed asking for a raise, spreadsheet and all. The "
             "spreadsheet was more confident than I was.",
     "when": {"all": [{"flag": "employed", "is": True}]}},
    {"id": "adult_firedd_1", "band": "adult",
     "text": "I am still between jobs. I am choosing to call it a "
             "sabbatical until further notice.",
     "when": {"all": [{"flag": "unemployed", "is": True},
                      {"stat": "happiness", "max": 55}]}},
    {"id": "adult_parentcall_1", "band": "adult",
     "text": "I called my mother and we talked about nothing for an "
             "hour. It fixed something I hadn't noticed was loose.",
     "when": {"all": [{"flag": "mother", "is": True},
                      {"role_relation": "mother", "min": 60}]}},
    {"id": "adult_parentold_1", "band": "adult",
     "text": "My parents are getting older and I find myself memorising "
             "the sound of their voices.",
     "when": {"all": [{"flag": "mother", "is": True}]}},
    {"id": "adult_frienddinner_1", "band": "adult",
     "text": "Old friends came to dinner and we laughed until the "
             "neighbours tapped the wall. Tradition upheld."},
    {"id": "adult_hobby_1", "band": "adult",
     "text": "I took up a hobby purely because it has no point. Highly "
             "recommend having one pointless thing."},
    {"id": "adult_car_1", "band": "adult",
     "text": "My car made a noise and the mechanic translated it into a "
             "number. An expensive dialect."},
    {"id": "adult_plants_1", "band": "adult",
     "text": "I kept a plant alive for a whole year. It and I have an "
             "unspoken agreement: minimal effort, maximal loyalty."},
    {"id": "adult_nightin_1", "band": "adult",
     "text": "I cancelled plans to stay in and felt not one flicker of "
             "regret. This is what the brochures don't tell you about "
             "adulthood."},
    {"id": "adult_layoffscare_1", "band": "adult",
     "text": "There were layoff rumours at work and I updated my "
             "resumé for the first time in years. It needed the exercise.",
     "when": {"all": [{"flag": "employed", "is": True},
                      {"stat": "happiness", "max": 50}]}},
    {"id": "adult_holiday_1", "band": "adult",
     "text": "I finally took a holiday and spent two days learning how "
             "to do nothing. Passed with distinction."},
    {"id": "adult_diagnosis_1", "band": "adult",
     "text": "The doctor gave me some numbers to improve and I have been "
             "negotiating with my own lifestyle ever since.",
     "when": {"all": [{"stat": "health", "max": 55}]}},
    {"id": "adult_sidehustle_1", "band": "adult",
     "text": "My side project got its first genuine fan, who is not "
             "related to me and not just being polite. Loudly "
             "celebrated fan."},
    {"id": "adult_weddingguest_1", "band": "adult",
     "text": "I went to a wedding, danced badly, and caught the bouquet "
             "by accident. The photos are evidence."},
    {"id": "adult_furnish_1", "band": "adult",
     "text": "I bought a sofa and the delivery men and I are now on "
             "first-name terms with my staircase."},
    {"id": "adult_latework_1", "band": "adult",
     "text": "A deadline ate my month and I emerged on the other side "
             "paler and slightly feral but victorious.",
     "when": {"all": [{"flag": "employed", "is": True}]}},
    {"id": "adult_oldfriend_1", "band": "adult",
     "text": "I ran into an old friend and we picked up mid-sentence "
             "like the years were a commercial break."},
    {"id": "adult_lottolos_1", "band": "adult",
     "text": "I imagined winning the lottery and spent it all in my "
             "head for a full evening. Lovely evening."},
    {"id": "adult_skill_1", "band": "adult",
     "text": "I learned a new skill badly, then less badly. The "
             "intermediate stage of anything is a swamp."},

    # -------------------------------------------------------------- elder 65+
    {"id": "elder_retire_1", "band": "elder",
     "text": "My working days are behind me. I watched the sunrise "
             "from my own kitchen and it felt like a bonus level.",
     "when": {"all": [{"flag": "retired", "is": True}]}},
    {"id": "elder_garden_1", "band": "elder",
     "text": "I garden most mornings. The tomatoes and I have an "
             "agreement: I water, they mostly show up."},
    {"id": "elder_grandkid_1", "band": "elder",
     "text": "The family visited and left chaos and jam on "
             "everything. The house was too quiet by evening.",
     "when": {"all": [{"flag": "children", "is": True}]}},
    {"id": "elder_walks_1", "band": "elder",
     "text": "I take the same walk every day and it has never once been "
             "the same walk."},
    {"id": "elder_recipe_1", "band": "elder",
     "text": "I finally wrote down the family recipe the way it was "
             "meant, with a pinch that is exactly one pinch."},
    {"id": "elder_doctor_2", "band": "elder",
     "text": "My knees filed a complaint this winter. I answered with "
             "stairs taken slowly and with dignity.",
     "when": {"all": [{"stat": "health", "max": 60}]}},
    {"id": "elder_strong_1", "band": "elder",
     "text": "The doctor said whatever I am doing, keep doing it. I am "
             "doing nothing special, but with great consistency.",
     "when": {"all": [{"stat": "health", "min": 70}]}},
    {"id": "elder_oldphotos_1", "band": "elder",
     "text": "I found a box of old photographs and spent a whole evening "
             "being young again by appointment."},
    {"id": "elder_tech_1", "band": "elder",
     "text": "I learned the new phone and now give unsolicited tutorials "
             "at the community centre."},
    {"id": "elder_nap_1", "band": "elder",
     "text": "I have perfected the afternoon nap: twenty minutes, one "
             "blanket, no mercy."},
    {"id": "elder_oldfriends_1", "band": "elder",
     "text": "We old friends meet every week and solve the world's "
             "problems over tea. The world has not noticed yet."},
    {"id": "elder_pension_1", "band": "elder",
     "text": "I counted my blessings this year on paper. The page ran "
             "out before the list did."},
    {"id": "elder_kitchen_1", "band": "elder",
     "text": "I still cook properly most nights. A good meal is a small "
             "daily argument against hurry."},
    {"id": "elder_birthdaybig_1", "band": "elder",
     "text": "Another big birthday came and went, attended by cake and a "
             "card that played music slightly too long."},
    {"id": "elder_letters_1", "band": "elder",
     "text": "I wrote letters to people I love, on paper. Some habits "
             "deserve to survive every technology."},
    {"id": "elder_grandkidteach_1", "band": "elder",
     "text": "I took a young relative fishing, or perhaps they took "
             "me. Patience all round. The fish remain uncommitted.",
     "when": {"all": [{"flag": "children", "is": True}]}},
    {"id": "elder_morning_1", "band": "elder",
     "text": "I wake early and sit with the quiet before the day starts. "
             "It is the cheapest luxury I know."},
    {"id": "elder_reminisce_1", "band": "elder",
     "text": "Someone asked about my first car and forty minutes just "
             "disappeared. Wonderful minutes."},
    {"id": "elder_volunteer2_1", "band": "elder",
     "text": "I volunteer at the school and the children ask the best "
             "questions, including several I finally get to answer."},
    {"id": "elder_checkup2_1", "band": "elder",
     "text": "The clinic visits are more frequent now, and I bring the "
             "nurse gossip like an offering.",
     "when": {"all": [{"flag": "ill", "is": True}]}},
    {"id": "elder_partner_1", "band": "elder",
     "text": "We had breakfast in the garden and said very little. "
             "Long love, I have learned, is mostly comfortable "
             "silences.",
     "when": {"all": [{"flag": "partner", "is": True}]}},
    {"id": "elder_widow_1", "band": "elder",
     "text": "I talked to an old photograph today. It went about as "
             "well as it ever does. I keep talking anyway.",
     "when": {"all": [{"flag": "partner", "is": False}]}},
    {"id": "elder_widow2_1", "band": "elder",
     "text": "I set out a second cup out of habit, then used it for "
             "pencils. Small bargains, made daily.",
     "when": {"all": [{"flag": "partner", "is": False}]}},
    {"id": "elder_storytelling_1", "band": "elder",
     "text": "I told the story of the great storm at a family dinner and "
             "watched it grow two feet taller in the telling."},
    {"id": "elder_moneywise_1", "band": "elder",
     "text": "I give careful advice about money now, from a lifetime of "
             "both saving it and watching it burn."},
    {"id": "elder_happy_1", "band": "elder",
     "text": "Happiness, I have decided, is mostly noticing. I notice "
             "everything these days.",
     "when": {"all": [{"stat": "happiness", "min": 70}]}},
]

# How many recent picks to exclude (guarantees no repeats within the window;
# the pool per band comfortably exceeds it).
RECENT_WINDOW = 60


# ---------------------------------------------------------------------------
# Selection
# ---------------------------------------------------------------------------

def _flags(player, context):
    relations = [r for r in getattr(player, "relations", []) or []
                 if getattr(r, "alive", True)]
    work = (context or {}).get("work") or {}
    education = (context or {}).get("education") or {}
    return {
        "employed": bool(work.get("employed")),
        "unemployed": not bool(work.get("employed")),
        "partner": getattr(player, "partner", None) is not None,
        "children": bool(getattr(player, "children", None)),
        "mother": any(r.__class__.__name__ == "Parent" and
                      r.gender == Gender.Female for r in relations),
        "father": any(r.__class__.__name__ == "Parent" and
                      r.gender == Gender.Male for r in relations),
        "ill": bool(getattr(player, "illnesses", None)),
        "dropped_out": bool(getattr(player, "dropped_out", False)),
        "retired": bool(work.get("retired")),
        "_school": education.get("stage") or "none",
    }


def _relation_value(player, role):
    """Best (max) relationship value among relations of the given role
    class name; 'any' across all relations."""
    from src.people.classes.parent import Parent
    from src.people.classes.sibling import Sibling
    from src.people.classes.partner import Partner
    from src.people.classes.child import Child
    classes = {"mother": (Parent, Gender.Female),
               "father": (Parent, Gender.Male),
               "sibling": (Sibling, None), "partner": (Partner, None),
               "child": (Child, None), "any": (object, None)}
    cls, gender = classes[role]
    best = None
    for r in getattr(player, "relations", []) or []:
        if not isinstance(r, cls):
            continue
        if gender is not None and getattr(r, "gender", None) != gender:
            continue
        value = getattr(r, "relationship", None)
        if value is None:
            continue
        best = value if best is None else max(best, value)
    return best


def _cond_ok(cond, player, flags):
    if "stat" in cond:
        value = getattr(player, cond["stat"], None)
        if not isinstance(value, (int, float)):
            return False
        if "min" in cond and value < cond["min"]:
            return False
        if "max" in cond and value > cond["max"]:
            return False
        return True
    if "flag" in cond:
        return bool(flags.get(cond["flag"])) == cond.get("is", True)
    if "school" in cond:
        return flags["_school"] == cond["school"]
    if "role_relation" in cond:
        value = _relation_value(player, cond["role_relation"])
        if value is None:
            return False
        if "min" in cond and value < cond["min"]:
            return False
        if "max" in cond and value > cond["max"]:
            return False
        return True
    return False


def _line_ok(line, player, flags):
    when = line.get("when") or {}
    conds = when.get("all") or []
    if any(not _cond_ok(c, player, flags) for c in conds):
        return False
    any_ = when.get("any") or []
    if any_ and not any(_cond_ok(c, player, flags) for c in any_):
        return False
    return True


def pick_year_line(player, context, recent, rng, pool=None):
    """Choose a flavour line for this year's journal entry.

    player:  the engine Player (observer-safe attributes only)
    context: {"education": _education_view(player),
              "work": _work_view(player)} — supplied by the caller
    recent:  list of recently used line ids (mutated by the caller side)
    rng:     cosmetic-stream rng (never the simulation stream)
    pool:    optional caller-supplied line list (same entry shape as
             LINES: id/band/text/when). Coordinated extension (M3
             amendment): A's relationship-drift hook passes its own
             pool; None keeps the default LINES pool.

    Returns (line_id, text) or None when nothing fits (caller keeps its
    default entry). Never returns an id present in `recent` while the
    band's eligible pool has any other member.
    """
    if rng is None:
        return None
    band = band_for_age(getattr(player, "age", None))
    if band is None:
        return None
    recent = set(recent or [])
    flags = _flags(player, context)
    pool = LINES if pool is None else pool
    pool = [l for l in pool if l["band"] == band]
    eligible = [l for l in pool if _line_ok(l, player, flags)
                and l["id"] not in recent]
    if not eligible:
        # unconditional fallback within the band, still de-duplicated
        eligible = [l for l in pool if not l.get("when")
                    and l["id"] not in recent]
    if not eligible:
        return None
    line = rng.choice(eligible)
    return line["id"], line["text"]


# ---------------------------------------------------------------------------
# Engine hook (A side is one call — see contracts.md §Journal flavour)
# ---------------------------------------------------------------------------

def year_journal_line(env, context=None, rng=None):
    """env-aware wrapper: draws from the cosmetic stream, persists the
    recents window in env["journal_line_recents"], returns the text (or
    None). Must be called inside a command context (cosmetic rng mounted)
    so the draw never touches the simulation stream."""
    player = env["player"]
    if rng is None:
        from src.appearance import integration
        rng = integration._APP_RNG  # cosmetic stream; mounted per command
    recents = env.setdefault("journal_line_recents", [])
    picked = pick_year_line(player, context, recents, rng)
    if not picked:
        return None
    line_id, text = picked
    recents.append(line_id)
    del recents[:-RECENT_WINDOW]
    return text
