"""Local hardening: only reconstruct the game's known save classes."""
import collections
import pickle
import sys

import src.appearance.model  # must be imported before unpickling saves

_ALLOWED = {
    "src.lifesim_lib.lifesim_lib": {"Gender", "Trait", "TranslateMarker"},
    "src.lifesim_lib.social_media": {"SocialMedia"},
    "src.appearance.model": {"Appearance"},
    **{"src.people.classes." + name.lower(): {name} for name in
       ("Person", "Player", "Parent", "Sibling", "Relationship", "Partner", "Child")},
}

class SaveUnpickler(pickle.Unpickler):
    def find_class(self, module, name):
        if (module, name) == ("collections", "deque"):
            return collections.deque
        if name in _ALLOWED.get(module, ()) and module in sys.modules:
            return getattr(sys.modules[module], name)
        raise pickle.UnpicklingError(f"Blocked save global: {module}.{name}")

    def persistent_load(self, pid):
        raise pickle.UnpicklingError("Persistent save references are forbidden")

def load_save(stream):
    return SaveUnpickler(stream).load()
