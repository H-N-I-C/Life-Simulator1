import gettext
import os as _os
import sys as _sys

try:
    LANGUAGES = [
        dir
        for dir in _os.listdir("./locale")
        if _os.path.isdir(_os.path.abspath("./locale/" + dir))
    ]
except OSError:
    LANGUAGES = []

_ = lambda s: s

dir = _os.getcwd()
langs = {"en": None}

for lang in LANGUAGES:
    try:
        l = gettext.translation("lifesim", localedir=dir + "/locale", languages=[lang])
    except:
        pass
    else:
        langs[lang] = l

lang_map = {
    "en": "English",
    "es": "Español",
    "fr": "Français",
    "ko": "한국인",
    "bn": "বাংলা",
    "id": "bahasa Indonesia",
    "np": "नेपाली ",
}

names = []
codes = []
for lang in langs:
    codes.append(lang)
    names.append(lang_map.get(lang, lang))

lang = None


def select_language():
    """Prompt for a language and install it. Terminal entry point only.

    Importing this module never prompts: the default is English until the
    terminal entry point explicitly calls this function. With closed or
    non-interactive stdin the English default is kept silently.
    """
    global _, lang
    if not _sys.stdin or not _sys.stdin.isatty():
        return  # keep English default; server/imports must never prompt
    for i, name in enumerate(names):
        print(f"{i+1}. {name}")
    valid = False
    num = 1
    while not valid:
        valid = True
        try:
            num = int(input())
        except (ValueError, EOFError):
            valid = False
            continue
        if not 1 <= num <= len(codes):
            valid = False
    language = codes[num - 1]
    if language != "en":
        lang = langs[language]
        lang.install()
        _ = lang.gettext
    else:
        lang = None
