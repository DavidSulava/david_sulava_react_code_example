import React from 'react';
import { FieldRenderProps, FieldWrapper } from '@progress/kendo-react-form';
import { Input, NumericTextBox } from '@progress/kendo-react-inputs';
import { IntlProvider, LocalizationProvider } from '@progress/kendo-react-intl';
import { Error } from "@progress/kendo-react-labels";

const CInput: React.FC<FieldRenderProps> = (fieldRenderProps) => {
  const {
    reference,
    label,
    placeholder,
    name,
    type,
    multiple,
    className,
    labelClassName,
    disabled,
    valid,
    validationMessage,
    value,
    presetValue,
    readOnly,
    hide,
    isNumeric,
    format,
    min,
    max,
    step,
    locale,
    onChange,
    maxLength=100,
    ...others
  } = fieldRenderProps

  const inputErrorClass = valid ? "" : "input-error"
  const hiddenClass = hide ? "k-hidden" : ""
  const disabledInputClass = disabled ? "disabled-input" : ""

  return (
    <FieldWrapper>
      {label && (
        <label className={`${labelClassName ?? ""} ${inputErrorClass}`}>{label}</label>
      )}
      {
        !isNumeric ?
          <Input
            ref={reference ?? undefined}
            name={name}
            valid={valid}
            type={type ?? 'text'}
            maxLength={maxLength}
            multiple={multiple ?? false}
            value={value ?? presetValue}
            placeholder={placeholder ?? ""}
            onChange={onChange}
            className={`${className} ${inputErrorClass} ${hiddenClass} ${disabledInputClass} input-ordinary`}
            disabled={disabled}
            readOnly={readOnly ?? false}
            {...others}
          />
          :
          <LocalizationProvider language={locale}>
            <IntlProvider locale={locale}>
              <NumericTextBox
                ref={reference ?? undefined}
                name={name}
                placeholder={placeholder ?? ""}
                className={`${className} ${inputErrorClass} ${hiddenClass} ${disabledInputClass} input-ordinary`}
                disabled={disabled}
                format={format ?? ""}
                spinners={false}
                valid={valid}
                value={value}
                min={min}
                max={max}
                step={step}
                onChange={onChange}
              />
            </IntlProvider>
          </LocalizationProvider>
      }
      {!valid && validationMessage && <Error>{validationMessage}</Error>}
    </FieldWrapper>
  )
}

export default CInput