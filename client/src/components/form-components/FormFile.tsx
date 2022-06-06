import React from 'react';
import { FieldRenderProps, FieldWrapper } from '@progress/kendo-react-form';
import { Button } from 'react-bootstrap';
import { Error } from '@progress/kendo-react-labels';

export const FormFile: React.FC<FieldRenderProps> = (fieldRenderProps) => {
  const {
    label,
    btnText,
    name,
    className = '',
    labelClassName,
    disabled = false,
    valid,
    validationMessage,
    inputRef,
    multiple = false,
    chosenFiles = [],
    onFileSelect,
    onChange,
  } = fieldRenderProps

  const errorClassName = valid ? "" : "input-error"
  return (
    <FieldWrapper>
      {label && (
        <label className={`${labelClassName ?? ""} ${errorClassName}`}>{label}</label>
      )}

      <div className={`k-d-flex k-justify-content-between k-mt-1`}>
        <div className={`k-d-flex k-mr-1 ${className}`}>
          <Button className={"button"} type={'button'} onClick={onFileSelect} disabled={disabled}>
            {btnText}
          </Button>
          <input
            name={name}
            type="file"
            ref={inputRef}
            multiple={multiple}
            onChange={onChange}
            style={{display: "none"}}
          />
          {/*Chosen file names*/}
          {
            chosenFiles.length ? (
              <div className="chosen-files-container k-mt-1">
                {
                  chosenFiles.map((file: HTMLInputElement, index: number) => (
                    <span className="k-align-self-center" key={index}>
                      {" "}
                      {index > 0 ? ", " : ""} {file.name}{" "}
                    </span>
                  ))}
              </div>
            ) : ""
          }
        </div>
      </div>

      <div className="k-d-flex-row k-justify-content-stretch" style={{width: '100%'}}>
        {!valid && validationMessage && <Error>{validationMessage}</Error>}
      </div>
    </FieldWrapper>
  )
}