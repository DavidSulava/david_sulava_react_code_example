import React from "react"
import { FieldWrapper, FieldRenderProps } from "@progress/kendo-react-form"
import { Error, Hint } from '@progress/kendo-react-labels';
import { TextArea } from "@progress/kendo-react-inputs";

const CTextArea: React.FC<FieldRenderProps> = (fieldRenderProps) => {
  const {
    label,
    value,
    placeholder,
    name,
    className='',
    labelClassName,
    rows, cols,
    disabled,
    valid,
    validationMessage,
    max,
    onChange,
  } = fieldRenderProps

  const errorClassName = valid ? "" : "input-error"
  const disabledClassName = disabled ? "disabled-input" : ""

  return (
    <FieldWrapper>
      {label && (
        <label className={`${labelClassName ?? ""} ${errorClassName}`}>{label}</label>
      )}
      <TextArea
        valid={valid}
        name={name}
        value={value}
        placeholder={placeholder}
        onChange={onChange}
        maxLength={max}
        rows={rows ?? 5}
        cols={cols?? 30}
        className={`k-form-field-wrap ${className} ${errorClassName} ${disabledClassName}`}
        disabled={disabled}
      >
        {value}
      </TextArea>
      {
        max &&
        <Hint direction={"end"} style={{ position: "absolute", right:15 }}>
          {value?.length || 0} / {max}
        </Hint>
      }


      {!valid && validationMessage && <Error>{validationMessage}</Error>}
    </FieldWrapper>
  )
}

export default CTextArea
