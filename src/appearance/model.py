"""Appearance model v2: versioned dataclass with stable string IDs.

Mirrors the frozen contract in docs/collab/M0b-examples.json (Fluent 3D
emoji avatars). Keeps `seed` from v1 (drives deterministic aging);
person_id stays on the Person object (integration.ensure_person_id).
Age is NOT stored here; aging derives stages from age + seed.
"""

from dataclasses import dataclass, field

from . import catalog

SCHEMA_VERSION = 2


@dataclass
class Appearance:
    schema_version: int = SCHEMA_VERSION
    seed: int = 0
    base: str = "person"
    skin_tone: str = "default"
    hair: str = "default"
    facial_hair: str = "none"
    headwear: str = "none"
    badges: list = field(default_factory=list)
    background: str = "bg_01"
    look_override: str = "none"

    def to_dict(self):
        return {
            "schema_version": self.schema_version,
            "seed": self.seed,
            "base": self.base,
            "skin_tone": self.skin_tone,
            "hair": self.hair,
            "facial_hair": self.facial_hair,
            "headwear": self.headwear,
            "badges": list(self.badges),
            "background": self.background,
            "look_override": self.look_override,
        }

    # ---------------------------------------------------------------- v1
    @staticmethod
    def _migrate_v1(data):
        """v1 -> v2 (docs/collab/M0b-examples.json migration_v1_to_v2).

        Lossy but stable; keeps seed. `base` is 'person' here — integration
        refines it from the owning Person's gender when known.
        """
        v1 = dict(data)
        v1_accessories = v1.get("accessories", {})
        if not isinstance(v1_accessories, dict):
            v1_accessories = {}

        hair = "default"
        if v1.get("hair_texture") in catalog.V1_CURLY_TEXTURES:
            hair = "curly"
        elif v1.get("hair_color") in catalog.V1_RED_HAIR:
            hair = "red"
        elif v1.get("hair_style") == "bald":
            hair = "bald"
        elif v1.get("hair_color") in catalog.V1_WHITE_HAIR:
            hair = "white"

        badges = []
        if v1_accessories.get("glasses", "none") != "none":
            badges.append("glasses")
        if v1_accessories.get("earrings", "none") != "none":
            badges.append("earrings")
        if v1_accessories.get("headwear") in catalog.V1_BADGE_CAP_HAIR:
            badges.append("cap")

        seed = v1.get("seed", 0)
        if not isinstance(seed, int):
            seed = 0
        return {
            "schema_version": SCHEMA_VERSION,
            "seed": seed,
            "base": "person",
            "skin_tone": catalog.V1_SKIN_MAP.get(
                v1.get("skin_tone"), "default"),
            "hair": hair,
            "facial_hair": "beard" if v1.get("facial_hair") not in
                           (None, "none") else "none",
            "headwear": "headscarf"
                        if v1_accessories.get("headwear") == "headscarf"
                        else "none",
            "badges": badges[:catalog.BADGE_LIMIT],
            "background": "bg_{:02d}".format(abs(seed) % 10 + 1),
            "look_override": "none",
        }

    @classmethod
    def from_dict(cls, data):
        """Tolerant loader: unknown/missing IDs fall back to defaults.

        v1 dicts are migrated to v2. Raises ValueError for unsupported
        schema versions (values < 1 or newer than SCHEMA_VERSION) so the
        loader can generate a fresh appearance instead of misinterpreting.
        """
        if not isinstance(data, dict):
            return cls()
        version = data.get("schema_version", SCHEMA_VERSION)
        if (not isinstance(version, int) or isinstance(version, bool)
                or version < 1 or version > SCHEMA_VERSION):
            raise ValueError(
                f"Unsupported appearance schema version: {version!r}")
        if version < SCHEMA_VERSION:
            data = cls._migrate_v1(data)
        kwargs = {}
        for f in ("base", "skin_tone", "hair", "facial_hair", "headwear",
                  "background", "look_override"):
            kwargs[f] = catalog.normalize_id(f, data.get(f))
        seed = data.get("seed", 0)
        kwargs["seed"] = seed if isinstance(seed, int) else 0
        kwargs["badges"] = catalog.normalize_multi(
            "badges", data.get("badges", []))
        kwargs["schema_version"] = SCHEMA_VERSION
        return cls(**kwargs)

    def refine_base_from_gender(self, gender):
        """After a standalone v1 migration the base is 'person'; refine it
        from the owning Person's gameplay gender when known."""
        from src.lifesim_lib.lifesim_lib import Gender
        if self.base == "person" and gender in (Gender.Male, Gender.Female):
            self.base = "man" if gender == Gender.Male else "woman"
        return self
