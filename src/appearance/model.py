"""Appearance model: versioned dataclass with stable string IDs.

Mirrors the frozen JSON contract in docs/collab/04-appearance-contract.md.
Age is NOT stored here; rendering/ageing take age separately.
"""

from dataclasses import dataclass, field

from . import catalog

SCHEMA_VERSION = 1


@dataclass
class Appearance:
    schema_version: int = SCHEMA_VERSION
    seed: int = 0
    skin_tone: str = "skin_04"
    face_shape: str = "oval"
    eye_shape: str = "almond"
    eye_color: str = "brown_dark"
    brows: str = "soft"
    nose: str = "button"
    mouth: str = "smile"
    hair_texture: str = "straight"
    hair_color: str = "black"
    marks: list = field(default_factory=list)
    hair_style: str = "short_crop"
    facial_hair: str = "none"
    accessories: dict = field(default_factory=lambda: {
        "glasses": "none",
        "headwear": "none",
        "earrings": "none",
    })
    outfit_style: str = "tee"
    outfit_color: str = "blue"

    PHYSICAL_FIELDS = (
        "skin_tone", "face_shape", "eye_shape", "eye_color", "brows",
        "nose", "mouth", "hair_texture", "hair_color", "marks",
    )
    STYLING_FIELDS = (
        "hair_style", "facial_hair", "accessories", "outfit_style",
        "outfit_color",
    )

    def to_dict(self):
        return {
            "schema_version": self.schema_version,
            "seed": self.seed,
            "skin_tone": self.skin_tone,
            "face_shape": self.face_shape,
            "eye_shape": self.eye_shape,
            "eye_color": self.eye_color,
            "brows": self.brows,
            "nose": self.nose,
            "mouth": self.mouth,
            "hair_texture": self.hair_texture,
            "hair_color": self.hair_color,
            "marks": list(self.marks),
            "hair_style": self.hair_style,
            "facial_hair": self.facial_hair,
            "accessories": {
                "glasses": self.accessories.get("glasses", "none"),
                "headwear": self.accessories.get("headwear", "none"),
                "earrings": self.accessories.get("earrings", "none"),
            },
            "outfit_style": self.outfit_style,
            "outfit_color": self.outfit_color,
        }

    @staticmethod
    def _migrate_legacy(data):
        """Hook for migrating older schema versions. None exist yet."""
        return data

    @classmethod
    def from_dict(cls, data):
        """Tolerant loader: unknown/missing IDs fall back to defaults.

        Raises ValueError for unsupported schema versions (invalid values
        or versions newer than SCHEMA_VERSION) so the loader can generate
        a fresh appearance instead of misinterpreting the data.
        """
        if not isinstance(data, dict):
            return cls()
        version = data.get("schema_version", SCHEMA_VERSION)
        if (not isinstance(version, int) or isinstance(version, bool)
                or version < 1 or version > SCHEMA_VERSION):
            raise ValueError(
                f"Unsupported appearance schema version: {version!r}")
        if version < SCHEMA_VERSION:
            data = cls._migrate_legacy(data)
        kwargs = {}
        single_fields = (
            "skin_tone", "face_shape", "eye_shape", "eye_color", "brows",
            "nose", "mouth", "hair_texture", "hair_color", "hair_style",
            "facial_hair", "outfit_style", "outfit_color",
        )
        for f in single_fields:
            kwargs[f] = catalog.normalize_id(f, data.get(f))
        seed = data.get("seed", 0)
        kwargs["seed"] = seed if isinstance(seed, int) else 0
        kwargs["schema_version"] = version
        raw_marks = data.get("marks", [])
        if isinstance(raw_marks, (list, tuple)):
            kwargs["marks"] = [
                m for m in (catalog.normalize_id("marks", m) for m in raw_marks)
            ]
            # drop duplicates, keep order
            seen = set()
            kwargs["marks"] = [
                m for m in kwargs["marks"] if not (m in seen or seen.add(m))
            ]
        else:
            kwargs["marks"] = []
        raw_acc = data.get("accessories", {})
        if not isinstance(raw_acc, dict):
            raw_acc = {}
        kwargs["accessories"] = {
            cat: catalog.normalize_id(cat, raw_acc.get(cat))
            for cat in catalog.ACCESSORY_CATEGORIES
        }
        return cls(**kwargs)
