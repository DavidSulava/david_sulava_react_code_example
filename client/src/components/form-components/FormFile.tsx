import React, { useEffect, useState } from 'react';
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
    onFileSelect,
    presetFiles,
    onChange,
  } = fieldRenderProps

  const [selectedFiles, setSelectedFiles] = useState<File[]>([])
  const [fileNames, setFileNames] = useState<string[]>([])
  const errorClassName = valid ? "" : "input-error"

  useEffect(()=>{
    const selectedFileNames = selectedFiles.map(file=> file.name)
    setFileNames(selectedFileNames)
  }, [selectedFiles])
  useEffect(() => {
    if(presetFiles?.length)
      setFileNames([...presetFiles])
  }, [presetFiles])

  const onChangeInner = (event: React.ChangeEvent<HTMLInputElement>) => {
    const files = [...event?.target?.files||[]]
    setSelectedFiles(files)

    if(event?.target)
      event.target.value = ''
    onChange({value: files})
  }
  const onFileRemove = (fileName: string) => {
    setSelectedFiles(prev => {
      const updatedPrev = [...prev].filter(item => item.name !== fileName)||[]
      onChange({value: updatedPrev})
      return  updatedPrev
    })
  }

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
            onChange={onChangeInner}
            style={{display: "none"}}
          />
          {/*Chosen file names*/}
          {
            fileNames.length ? (
              <div className="chosen-files-container">
                {
                  fileNames.map((name, index: number) => (
                    <span className="file-row" key={index + name}>
                      {name} {" "}
                      <span className="k-icon k-i-close-outline close-icon" title="remove file" onClick={()=>onFileRemove(name)}></span>
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