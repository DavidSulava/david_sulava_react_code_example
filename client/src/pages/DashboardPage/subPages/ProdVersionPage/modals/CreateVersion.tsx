import { useDispatch, useSelector } from 'react-redux';
import React, { FC, useEffect, useRef, useState } from 'react';
import { Field, FieldWrapper, Form, FormElement } from '@progress/kendo-react-form';
import { IModalWrapperButton } from '../../../../../types/Modal';
import ModalWrapper from '../../../../../components/ModalWrapper/ModalWrapper';
import CInput from '../../../../../components/form-components/CInput';
import { isEmpty, isEmptyNoMsg } from '../../../../../components/form-components/helpers/valodation-functions';
import { Button } from 'react-bootstrap';
import { getAppBundle } from '../../../../../stores/common/reducer';
import { IState } from '../../../../../stores/configureStore';
import { postProdVerByProdId, putProdVer } from '../../../../../stores/productVersion/reducer';
import { IPostProductVersion, IProductVersion } from '../../../../../types/productVersion';
import { DropDownList } from '@progress/kendo-react-dropdowns';
import { ICommonModalProps } from '../../../../../types/common';
import { FormFile } from '../../../../../components/form-components/FormFile';

interface ICreateVerProps extends ICommonModalProps {
  productId: string,
  dataToUpdate?: IProductVersion,
}

//TODO: CreateVersion: доделать логику для PUT запроса
const CreateVersion: FC<ICreateVerProps> = ({
  isOpen,
  productId,
  dataToUpdate,
  onClose,
}) => {
  const dispatch = useDispatch()
  const appBundle = useSelector((state: IState) => state.common.appBundle)
  const formRef = useRef<Form|null>(null)
  const imageFilesRef = useRef<HTMLInputElement|null>(null)
  const modelFileRef = useRef<HTMLInputElement|null>(null)
  const formSubmitBtnRef = useRef<HTMLButtonElement|null>(null)

  const headerText = 'Create version';
  const [formState, setFormState] = useState({
    SequenceNumber: '',
    Name: '',
    Version: '',
    DesignGearVersion: '',
    InventorVersion: '',
    ProductId: productId,
    AppBundleId: appBundle[0]
  })
  const [chosenImgFiles, setChosenImgFiles] = useState<File[]>([])
  const [chosenModelFiles, setChosenModelFiles] = useState<File[]>([])
  const modalButtons: IModalWrapperButton[] = [
    {buttonText: "close", onButtonClick: () => onClose()},
    {buttonText: "save", onButtonClick: () => formSubmitBtnRef?.current?.click()}
  ]

  useEffect(() => {
    dispatch(getAppBundle())
  }, [])
  useEffect(() => {
    setFormState({...formState, AppBundleId: appBundle[0]})
  }, [appBundle])
  useEffect(() => {
    if(!dataToUpdate) return
    setFormState({
      ...formState,
      SequenceNumber: dataToUpdate.sequenceNumber.toString(),
      Name: dataToUpdate.name,
      Version: dataToUpdate.version,
      DesignGearVersion: dataToUpdate.designGearVersion,
      InventorVersion: dataToUpdate.inventorVersion,
      // AppBundleId: dataToUpdate.appBundleId,
    })
  }, [dataToUpdate])
  const onSubmitLocal = (formData: any) => {
    if(!formRef?.current?.isValid()) return

    const data = new FormData();
    data.append('SequenceNumber', formData.SequenceNumber)
    data.append('Name', formData.Name)
    data.append('Version', formData.Version)
    data.append('DesignGearVersion', formData.DesignGearVersion)
    data.append('InventorVersion', formData.InventorVersion)
    // data.append('DesignGearVersion', formData.AppBundleId.designGearVersion)
    // data.append('InventorVersion', formData.AppBundleId.inventorVersion)
    data.append('ProductId', formData.ProductId)
    data.append('AppBundleId', formData.AppBundleId.id)
    data.append('ModelFile', chosenModelFiles[0])
    chosenImgFiles.forEach(item => data.append('ImageFiles', item))

    if(!dataToUpdate)
      dispatch(postProdVerByProdId(data as IPostProductVersion))
    else{
      data.append('id', dataToUpdate.id)
      dispatch(putProdVer(data as IPostProductVersion))
    }
    onClose()
  }
  const onFileSelect = () => {
    imageFilesRef?.current?.click()
  }
  const onModelSelect = () => {
    modelFileRef?.current?.click()
  }
  const onFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    if(!event.target.files) return
    const fileUploaded = event.target.files
    setChosenImgFiles([...fileUploaded])
  }
  const onModelChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    if(!event.target.files) return
    const fileUploaded = event.target.files
    setChosenModelFiles([...fileUploaded])
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
            <fieldset className={"k-form-fieldset"}>
              <div className="mb-3">
                <Field
                  name={"SequenceNumber"}
                  component={CInput}
                  label={"Sequence Number"}
                  validator={isEmpty}
                />
              </div>
              <div className="mb-3">
                <Field
                  name={"Name"}
                  component={CInput}
                  label={"Version name"}
                  validator={isEmpty}
                />
              </div>
              <div className="mb-3">
                <Field
                  name={"Version"}
                  component={CInput}
                  label={"Version"}
                  validator={isEmpty}
                />
              </div>
              <div className="mb-3">
                <Field
                  name={"DesignGearVersion"}
                  component={CInput}
                  label={"Design gear version"}
                  validator={isEmpty}
                />
              </div>
              <div className="mb-3">
                <Field
                  name={"InventorVersion"}
                  component={CInput}
                  label={"Inventor version"}
                  validator={isEmpty}
                />
              </div>
              <div className="mb-3">
                <div>Select app bundle</div>
                <FieldWrapper>
                  <DropDownList
                    name="AppBundleId"
                    style={{width: "50%"}}
                    data={appBundle}
                    textField="name"
                    dataItemKey="id"
                    defaultValue={appBundle[0]}
                    onChange={val => formRef?.current?.valueSetter("AppBundleId", val.target.value)}
                    required={true}
                  />
                </FieldWrapper>

              </div>
              <div className="mb-3">
                <Field
                  name={"ImageFiles"}
                  component={FormFile}
                  className="upload-component"
                  label="Select images"
                  btnText={'select'}
                  multiple={true}
                  inputRef={imageFilesRef}
                  chosenFiles={chosenImgFiles}
                  onFileSelect={onFileSelect}
                  onChange={onFileChange}
                  validator={isEmptyNoMsg}
                />
              </div>
              <div className="mb-3">
                <Field
                  name={"ModelFile"}
                  component={FormFile}
                  className="upload-component"
                  label="Select model"
                  btnText={'select'}
                  inputRef={modelFileRef}
                  chosenFiles={chosenModelFiles}
                  onFileSelect={onModelSelect}
                  onChange={onModelChange}
                  validator={isEmptyNoMsg}
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

export default CreateVersion