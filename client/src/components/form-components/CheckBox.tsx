import React from "react"
import { FieldRenderProps } from "@progress/kendo-react-form"
import { Checkbox, CheckboxChangeEvent } from "@progress/kendo-react-inputs"


const FormCheckBox = (fieldRenderProps: FieldRenderProps) => {
  const {
    id,
    label,
    className,
    icon,
    wrapperClassName,
    labelClassName,
    name,
    disabled,
    validationMessage,
    onCheckChange,
    onChange,
    ...others
  } = fieldRenderProps

  const disabledElementClass = disabled ? "disabled-text-color" : ""

  const onCheck = (event: CheckboxChangeEvent) => {
    onChange(event)

    const checkBoxParams = {
      name: event.target?.element?.name,
      value: event.value,
    }
    if (onCheckChange) onCheckChange(checkBoxParams)
  }

  return (
    <div className={wrapperClassName}>
      <Checkbox
        id={id}
        name={name}
        className={className}
        disabled={disabled}
        onChange={onCheck}
        {...others}
      />

      {label && (
        <span className="k-d-inline-flex k-ml-2">
          <span>
            <label
              className={`${labelClassName ?? ""} ${disabledElementClass}`}
              htmlFor={id}
            >
            {label}
          </label>
          </span>
          {icon}
        </span>
      )}
    </div>
  )
}

export default FormCheckBox
