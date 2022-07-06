import { useDispatch, useSelector } from 'react-redux';
import React, { FC, useEffect, useRef, useState } from 'react';
import { Field, FieldWrapper, Form, FormElement } from '@progress/kendo-react-form';
import { IModalWrapperButton } from '../../../../../types/modal';
import ModalWrapper from '../../../../../components/ModalWrapper/ModalWrapper';
import CInput from '../../../../../components/form-components/CInput';
import {
  isEmpty,
  isImage,
  isNumberErrorMsg,
  isZip
} from '../../../../../components/form-components/helpers/validation-functions';
import { Button } from 'react-bootstrap';
import { IState } from '../../../../../stores/configureStore';
import {
  getProdVersion,
  postProdVerByProdId,
  putProdVer,
  setProdVersion,
} from '../../../../../stores/productVersion/reducer';
import { IPostProductVersion } from '../../../../../types/productVersion';
import { DropDownList } from '@progress/kendo-react-dropdowns';
import { ICommonModalProps, IKendoOnChangeEvent } from '../../../../../types/common';
import { FormFile } from '../../../../../components/form-components/FormFile';
import useProdVersion from '../../../../../helpers/hooks/storeHooks/useProdVersion';
import FormCheckBox from '../../../../../components/form-components/CheckBox';
import { getAppBundleList } from '../../../../../stores/appBundle/reducer';
import { IAppBundle } from '../../../../../types/appBundle';
import Spinner from '../../../../../components/Loaders/Spinner/Spinner';
import { setPostReqResp } from '../../../../../stores/common/reducer';

interface ICreateVerProps extends ICommonModalProps {
  productId: string,
  dataToUpdateId?: string,
}

//TODO: CreateVersion: доделать логику для PUT запроса,
// изменить логику для картинок , когда будут приходить настоящие данные с картинками
const CreateVersion: FC<ICreateVerProps> = ({
  isOpen,
  productId,
  dataToUpdateId,
  onClose,
}) => {
  const dispatch = useDispatch()
  const {prodVersion, isProdVersionLoading} = useProdVersion()
  const backEndResp = useSelector((state: IState) => state.common.postReqResp)
  const appBundleList = useSelector((state: IState) => state.appBundle.appBundleList)
  const formRef = useRef<Form|null>(null)
  const imageFilesRef = useRef<HTMLInputElement|null>(null)
  const modelFileRef = useRef<HTMLInputElement|null>(null)
  const formSubmitBtnRef = useRef<HTMLButtonElement|null>(null)

  const headerText = dataToUpdateId ? 'Update version' : 'Create version';
  const [formState, setFormState] = useState<any>({
    SequenceNumber: '',
    Name: '',
    Version: '',
    DesignGearVersion: '',
    InventorVersion: '',
    IsCurrent: false,
    ProductId: productId,
    AppBundleId: appBundleList?.data[0]
  })
  const [chosenImgFiles, setChosenImgFiles] = useState<File[]>([])
  const [preloadedImgFiles, setPreloadedImgFiles] = useState<string[]>([])
  const [chosenModelFiles, setChosenModelFiles] = useState<File[]>([])
  const [defaultBundle, setDefaultBundle] = useState<IAppBundle|null>(null)
  const modalButtons: IModalWrapperButton[] = [
    {buttonText: 'close', onButtonClick: () => onClose()},
    {buttonText: 'save', onButtonClick: () => formSubmitBtnRef?.current?.click(), buttonDisabled: isProdVersionLoading  }
  ]

  useEffect(() => {
    dispatch(getAppBundleList())
    return () => {
      dispatch(setProdVersion(null))
    }
  }, [])
  useEffect(() => {
    if(prodVersion?.appBundleId && appBundleList?.data.length) {
      const presetBundle = appBundleList.data.find(item => item.id === prodVersion?.appBundleId)
      const bundle = presetBundle || appBundleList.data[0]
      formRef?.current?.valueSetter("AppBundleId", bundle)
      setDefaultBundle(bundle)
    }
    else {
      formRef?.current?.valueSetter("AppBundleId", appBundleList?.data[0])
      setDefaultBundle(appBundleList?.data[0] || null)
    }

  }, [appBundleList])
  useEffect(() => {
    if(dataToUpdateId) {
      dispatch(setProdVersion(null))
      dispatch(getProdVersion(dataToUpdateId))
    }
  }, [dataToUpdateId])
  useEffect(() => {
    if(prodVersion) {
      setFormState({
        ...formState,
        SequenceNumber: prodVersion.sequenceNumber.toString(),
        Name: prodVersion.name,
        Version: prodVersion.version,
        DesignGearVersion: prodVersion.designGearVersion,
        InventorVersion: prodVersion.inventorVersion,
        IsCurrent: prodVersion.isCurrent,
      })
      if(!dataToUpdateId)
        setFormState({...formState, UseAsTemplateConfiguration: false})
      setPreloadedImgFiles(prodVersion.imageFiles)
      dispatch(getAppBundleList())
    }
  }, [prodVersion])
  useEffect(() => {
    if(backEndResp){
      onClose()
      dispatch(setPostReqResp(''))
    }
  }, [backEndResp, isProdVersionLoading])

  const onSubmitLocal = (formData: any) => {
    if(!formRef?.current?.isValid()) return

    const data = new FormData();
    data.append('SequenceNumber', formData.SequenceNumber)
    data.append('Name', formData.Name)
    data.append('Version', formData.Version)
    data.append('DesignGearVersion', formData.DesignGearVersion)
    data.append('InventorVersion', formData.InventorVersion)
    data.append('ProductId', formData.ProductId)
    data.append('AppBundleId', formData.AppBundleId.id)
    data.append('IsCurrent', formData.IsCurrent)
    if(chosenModelFiles.length)
      data.append('ModelFile', chosenModelFiles[0])
    if(chosenImgFiles.length)
      chosenImgFiles.forEach(item => data.append('ImageFiles', item))

    if(!dataToUpdateId){
      data.append('UseAsTemplateConfiguration', formData.UseAsTemplateConfiguration)
      dispatch(postProdVerByProdId(data as IPostProductVersion))
    }
    else {
      data.append('id', dataToUpdateId)
      dispatch(putProdVer(data as IPostProductVersion))
    }
  }
  const onFileSelect = () => {
    imageFilesRef?.current?.click()
  }
  const onModelSelect = () => {
    modelFileRef?.current?.click()
  }
  const onFileChange = (event: IKendoOnChangeEvent) => {
    const fileUploaded = event?.value
    setChosenImgFiles([...fileUploaded])

    if(preloadedImgFiles.length)
      setPreloadedImgFiles([])
  }
  const onModelChange = (event: IKendoOnChangeEvent) => {
    const fileUploaded = event.value
    setChosenModelFiles([...fileUploaded])
  }
  const innerValidateImg = (val: any) => {
    if(formRef?.current)
      formRef.current.modified['ImageFiles'] = true
    return !preloadedImgFiles.length ? isImage(val) : ''
  }

  return (
    <ModalWrapper
      headerText={headerText}
      isOpen={isOpen}
      onClose={onClose}
      buttons={modalButtons}
      className={'create-version-form'}
    >

      <Form
        onSubmit={onSubmitLocal}
        initialValues={formState}
        key={JSON.stringify(formState)}
        ref={formRef}
        render={(formRenderProps) => (
          <FormElement className={'modal-body'}>
            {
              isProdVersionLoading ? <Spinner/> :
                <fieldset className={"k-form-fieldset"}>
                  <div className="mb-3">
                    <Field
                      name="SequenceNumber"
                      component={CInput}
                      label="Sequence Number"
                      validator={isNumberErrorMsg}
                      maxLength={10}
                    />
                  </div>
                  <div className="mb-3">
                    <Field
                      name="Name"
                      component={CInput}
                      label="Version name"
                      validator={isEmpty}
                    />
                  </div>
                  <div className="mb-3">
                    <Field
                      name="Version"
                      component={CInput}
                      label="Version"
                      validator={isNumberErrorMsg}
                      maxLength={10}
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
                      name="IsCurrent"
                      id="IsCurrent"
                      component={FormCheckBox}
                      label="default version"
                    />
                    {
                      !dataToUpdateId &&
                      <Field
                        name="UseAsTemplateConfiguration"
                        id="UseAsTemplateConfiguration"
                        component={FormCheckBox}
                        label="set as template configuration"
                      />
                    }
                  </div>
                  <div className="mb-3">
                    <div>Select app bundle</div>
                    <FieldWrapper>
                      <DropDownList
                        name="AppBundleId"
                        style={{width: "50%"}}
                        data={appBundleList?.data}
                        textField="name"
                        dataItemKey="id"
                        defaultValue={defaultBundle}
                        loading={!appBundleList?.data.length}
                        onChange={val => formRef?.current?.valueSetter("AppBundleId", val.target.value)}
                        required={true}
                      />
                    </FieldWrapper>

                  </div>
                  <div className="mb-3">
                    <Field
                      name="ImageFiles"
                      component={FormFile}
                      className="upload-component"
                      label="Select images"
                      btnText="select"
                      multiple={true}
                      inputRef={imageFilesRef}
                      presetFiles={preloadedImgFiles || []}
                      onFileSelect={onFileSelect}
                      onChange={onFileChange}
                      validator={innerValidateImg}
                    />
                  </div>
                  {
                    !dataToUpdateId &&
                    <div className="mb-3">
                      <Field
                        name="ModelFile"
                        component={FormFile}
                        className="upload-component"
                        label="Select model"
                        btnText="select"
                        inputRef={modelFileRef}
                        onFileSelect={onModelSelect}
                        onChange={onModelChange}
                        validator={isZip}
                      />
                    </div>
                  }

                </fieldset>
            }
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

export default CreateVersion