import { ICommonModalProps, IKendoOnChangeEvent } from '../../../../../types/common';
import React, { FC, useEffect, useRef, useState } from 'react';
import { useDispatch } from 'react-redux';
import { Field, Form, FormElement } from '@progress/kendo-react-form';
import { IModalWrapperButton } from '../../../../../types/modal';
import ModalWrapper from '../../../../../components/ModalWrapper/ModalWrapper';
import CInput from '../../../../../components/form-components/CInput';
import { isEmpty, isNumberErrorMsg, isZip } from '../../../../../components/form-components/helpers/validation-functions';
import { FormFile } from '../../../../../components/form-components/FormFile';
import { Button } from 'react-bootstrap';
import { getAppBundleById, postAppBundle, putAppBundle } from '../../../../../stores/appBundle/reducer';
import { IPostAppBundle, IPuttAppBundle } from '../../../../../types/appBundle';
import useAppBundle from '../../../../../helpers/hooks/useAppBundle';
import CTextArea from '../../../../../components/form-components/CTextArea';

interface ICreateAppBundle extends ICommonModalProps {
  dataToUpdateId?: string,
}

//TODO: CreateAppBundleModal: доделать логику для PUT запроса,
// изменить логику для выбранного файла , когда будут приходить настоящие данные с файлом
const CreateAppBundleModal: FC<ICreateAppBundle> = ({
  isOpen,
  dataToUpdateId,
  onClose,
}) => {
  const dispatch = useDispatch()
  const {appBundle, isBundleLoading} = useAppBundle()
  const formRef = useRef<Form|null>(null)
  const bundleFileRef = useRef<HTMLInputElement|null>(null)
  const formSubmitBtnRef = useRef<HTMLButtonElement|null>(null)

  const headerText = dataToUpdateId ? 'Update Bundle' : 'Create Bundle';
  const [formState, setFormState] = useState({
    Name: '',
    Description: '',
    DesignGearVersion: '',
    InventorVersion: '',
    File: '',
  })
  const [chosenBundleFiles, setChosenBundleFiles] = useState<string[]>([])
  const [preloadedBundleFile, setPreloadedBundleFile] = useState<string>()
  const modalButtons: IModalWrapperButton[] = [
    {buttonText: 'close', onButtonClick: () => onClose()},
    {buttonText: 'save', onButtonClick: () => formSubmitBtnRef?.current?.click()}
  ]

  useEffect(() => {
    if(isOpen && dataToUpdateId) {
      dispatch(getAppBundleById(dataToUpdateId))
    }
    else {
      setFormState({
        Name: '',
        Description: '',
        DesignGearVersion: '',
        InventorVersion: '',
        File: '',
      })
      setChosenBundleFiles([])
      setPreloadedBundleFile('')
    }
  }, [isOpen])
  useEffect(() => {
    if(appBundle) {
      setFormState({
        Name: appBundle.name,
        Description: appBundle.description,
        DesignGearVersion: appBundle.designGearVersion,
        InventorVersion: appBundle.inventorVersion,
        File: '',
      })
      setPreloadedBundleFile(appBundle?.fileName)
    }

  }, [appBundle])
  const onSubmitLocal = (formData: any) => {
    if(!formRef?.current?.isValid()) return

    const data = new FormData();
    data.append('Name', formData.Name)
    data.append('Description', formData.Description)
    data.append('DesignGearVersion', formData.DesignGearVersion)
    data.append('InventorVersion', formData.InventorVersion)

    if(chosenBundleFiles.length)
      data.append('File', chosenBundleFiles[0])

    if(!dataToUpdateId)
      dispatch(postAppBundle(data as IPostAppBundle))
    else {
      data.append('id', dataToUpdateId)
      dispatch(putAppBundle(data as IPuttAppBundle))
    }
    onClose()
  }
  const onBundleFileSelect = () => {
    bundleFileRef?.current?.click()
  }
  const onBundleFileChange = (event: IKendoOnChangeEvent) => {
    const fileUploaded = event.value
    setChosenBundleFiles([...fileUploaded])
    if(preloadedBundleFile)
      setPreloadedBundleFile(undefined)
  }
  const innerValidateFile = (val: any) => {
    if(formRef?.current)
      formRef.current.modified['File'] = true
    return !preloadedBundleFile ? isZip(val) : ''
  }

  return (
    <ModalWrapper
      headerText={headerText}
      isOpen={isOpen}
      onClose={onClose}
      buttons={modalButtons}
      className={'create-version-form create-bundle-form'}
    >
      <Form
        onSubmit={onSubmitLocal}
        initialValues={formState}
        key={JSON.stringify(formState)}
        ref={formRef}
        render={(formRenderProps) => (
          <FormElement className={'modal-body'}>
            <fieldset className={"k-form-fieldset"}>
              <div className="mb-3">
                <Field
                  name="Name"
                  component={CInput}
                  label="Bundle name"
                  validator={isEmpty}
                />
              </div>
              <div className="mb-3">
                <Field
                  name="Description"
                  label="Description"
                  component={CTextArea}
                  max={200}
                  cols={25}
                  rows={2}
                  validator={isEmpty}
                />
              </div>
              <div className="mb-3">
                <Field
                  name="DesignGearVersion"
                  component={CInput}
                  label="Design gear version"
                  validator={isNumberErrorMsg}
                  maxLength={10}
                />
              </div>
              <div className="mb-3">
                <Field
                  name="InventorVersion"
                  component={CInput}
                  label="Inventor version"
                  validator={isNumberErrorMsg}
                  maxLength={10}
                />
              </div>
              <div className="mb-3">
                <Field
                  name="File"
                  component={FormFile}
                  className="upload-component"
                  label="Select file"
                  btnText="select"
                  inputRef={bundleFileRef}
                  presetFiles={preloadedBundleFile ? [preloadedBundleFile] : []}
                  onFileSelect={onBundleFileSelect}
                  onChange={onBundleFileChange}
                  validator={innerValidateFile}
                />
              </div>
            </fieldset>

            <Button
              type="submit"
              hidden={true}
              ref={formSubmitBtnRef}
            >
              submit
            </Button>
          </FormElement>
        )}
      />
    </ModalWrapper>
  )
}

export default CreateAppBundleModal