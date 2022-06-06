import { ICommonModalProps } from '../../../../../types/common';
import React, { FC, useEffect, useRef, useState } from 'react';
import { Field, FieldWrapper, Form, FormElement } from '@progress/kendo-react-form';
import { IModalWrapperButton } from '../../../../../types/modal';
import ModalWrapper from '../../../../../components/ModalWrapper/ModalWrapper';
import CInput from '../../../../../components/form-components/CInput';
import { isEmpty, isEmptyNoMsg } from '../../../../../components/form-components/helpers/valodation-functions';
import { Button } from 'react-bootstrap';
import { useParams } from 'react-router-dom';
import useProdVersion from '../../../../../helpers/hooks/useProdVersion';
import { useDispatch, useSelector } from 'react-redux';
import { IState } from '../../../../../stores/configureStore';
import { getConfigParams, getConfigurations, searchConfiguration } from '../../../../../stores/productConfigurations/reducer';
import { ComboBox, DropDownList } from '@progress/kendo-react-dropdowns';
import { enumToKeqValue } from '../../../../../helpers/enumFunctions';
import useConfigurations from '../../../../../helpers/hooks/useConfigurations';
import { EConfigurationStatus, ESvfStatus, IConfigurations } from '../../../../../types/producVersionConfigurations';
import SelectWithSearch from '../../../../../components/form-components/SelecrWithSerach';
import CSelectWithSearch from '../../../../../components/form-components/SelecrWithSerach';

const configStates = enumToKeqValue(EConfigurationStatus)
const svfStatuses = enumToKeqValue(ESvfStatus)


const CreateConfigModal: FC<ICommonModalProps> = ({
  isOpen,
  onClose,
}) => {
  const dispatch = useDispatch()
  const {organizationId} = useParams()
  const {prodVersion} = useProdVersion()
  const {isConfigLoading, configurationsList, searchedConfigList, configParams} = useConfigurations()
  const appBundle = useSelector((state: IState) => state.common.appBundle)
  const formRef = useRef<Form>(null)
  const formSubmitBtnRef = useRef<HTMLButtonElement>(null)
  const headerText = 'Create configuration';
  const [formState, setFormState] = useState({})
  const [selectedConfigs, setSelectedConfigs] = useState<IConfigurations[]>([])
  const modalButtons: IModalWrapperButton[] = [
    {buttonText: "close", onButtonClick: () => onClose()},
    {buttonText: "save", onButtonClick: () => formSubmitBtnRef?.current?.click()}
  ]

  useEffect(() => {
    if(prodVersion?.id)
      dispatch(getConfigurations(prodVersion?.id))
  }, [])
  useEffect(()=>{
    if(configurationsList?.data.length && isOpen){
      dispatch(getConfigParams(configurationsList.data[0].id))
    }
    if(isOpen){
      setFormState({
        organizationId: organizationId,
        productId: prodVersion?.productId,
        productVersionId: prodVersion?.id,
        appBundleId: prodVersion?.appBundleId,
        status: configStates[0],
        svfStatus: svfStatuses[0],
        name: '',
        comment: '',
        baseConfigurationId: '',
        parameterValues: [],
      })
    }
  }, [isOpen])
  useEffect(()=>{
    setSelectedConfigs(searchedConfigList?.data||configurationsList?.data||[])
  }, [searchedConfigList])

  const onSubmitLocal = (formData: any) => {
    if(!formRef?.current?.isValid()) return
    onClose()
  }
  const searchConfigs = (skip = 0, take= 5, filter='') => {
    dispatch(searchConfiguration({
      id: prodVersion?.id ?? '',
      value: filter,
      take: take
    }))
    return  ''
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
                  name={"name"}
                  component={CInput}
                  label={"Name"}
                  validator={isEmpty}
                />
              </div>
              <div className="mb-3">
                <Field
                  name={"comment"}
                  component={CInput}
                  label={"Comment"}
                  validator={isEmpty}
                />
              </div>
              <div className="mb-3">
                <Field
                  name={"baseConfigurationId"}
                  component={CSelectWithSearch}
                  label={'Select base configuration'}
                  data={selectedConfigs}
                  keyField={"id"}
                  textField={"configurationName"}
                  required={true}
                  loading={isConfigLoading}
                  onRequestData={searchConfigs}
                  validator={(val:any)=>isEmpty(val?.id)}
                />
              </div>
              <div className="mb-3">
                <FieldWrapper>
                  <div>Select status</div>
                  <DropDownList
                    name="status"
                    style={{width: "50%"}}
                    data={configStates}
                    textField="key"
                    dataItemKey="value"
                    defaultValue={configStates[0]}
                    onChange={val => formRef?.current?.valueSetter("status", val.target.value)}
                    required={true}
                  />
                </FieldWrapper>
              </div>
              <div className="mb-3">
                <FieldWrapper>
                  <div>Select SVF status</div>
                  <DropDownList
                    name="status"
                    style={{width: "50%"}}
                    data={svfStatuses}
                    textField="key"
                    dataItemKey="value"
                    defaultValue={svfStatuses[0]}
                    onChange={val => formRef?.current?.valueSetter("status", val.target.value)}
                    required={true}
                  />
                </FieldWrapper>
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

export default CreateConfigModal