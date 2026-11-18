/* v2 shared form-input helpers (Kimi B, redesign-v2). One implementation of
 * the frozen input shape (adapter normalizeInput: number/text/choice/
 * boolean) shared by the action modal and the choice sheet. Pure DOM; the
 * contract is docs/collab/redesign-v2/contracts.md §Choice. */

export function buildInputRow(input, doc) {
  const wrap = document.createElement("label");
  wrap.className = "field";
  const id = "in-" + input.id;
  let control;
  if (input.type === "choice" && input.choices) {
    control = document.createElement("select");
    for (const c of input.choices) {
      const o = document.createElement("option");
      o.value = typeof c === "object" ? c.id : c;
      o.textContent = typeof c === "object" ? c.label : c;
      control.appendChild(o);
    }
  } else if (input.type === "boolean") {
    control = document.createElement("input");
    control.type = "checkbox";
  } else {
    control = document.createElement("input");
    control.type = input.type === "number" ? "number" : "text";
    // 16px+ avoids iOS focus zoom so the sheet stays above the keyboard
    control.style.fontSize = "16px";
    if (input.type === "number") {
      if (input.min != null) control.min = input.min;
      if (input.max != null) control.max = input.max;
    } else if (input.max != null) {
      control.maxLength = input.max;
    }
  }
  control.id = id;
  control.required = !!input.required;
  wrap.appendChild(document.createTextNode(input.label +
    (input.required ? " *" : "")));
  wrap.appendChild(control);
  doc[input.id] = { control, input };
  return wrap;
}

export function collectInputs(doc) {
  const payload = {};
  for (const [key, { control, input }] of Object.entries(doc)) {
    let v;
    if (input.type === "boolean") v = control.checked;
    else if (input.type === "number") {
      if (control.value === "") {
        if (input.required) throw new Error(input.label + " is required");
        continue;
      }
      v = Number(control.value);
      if (!Number.isFinite(v)) throw new Error(input.label + " must be a number");
      if (input.min != null && v < input.min) {
        throw new Error(input.label + " must be at least " + input.min);
      }
      if (input.max != null && v > input.max) {
        throw new Error(input.label + " must be at most " + input.max);
      }
    } else {
      v = control.value.trim();
      if (input.required && !v) throw new Error(input.label + " is required");
      if (v && input.min != null && v.length < input.min) {
        throw new Error(input.label + " must be at least " + input.min +
                        " characters");
      }
      if (v && input.max != null && v.length > input.max) {
        throw new Error(input.label + " must be at most " + input.max +
                        " characters");
      }
    }
    payload[key] = v;
  }
  return payload;
}
