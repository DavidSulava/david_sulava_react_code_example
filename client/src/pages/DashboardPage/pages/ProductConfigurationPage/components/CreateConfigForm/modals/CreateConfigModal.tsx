import { ICommonModalProps } from '../../../../../../../types/common';
import React, { FC, useEffect, useRef, useState } from 'react';
import ModalWrapper from '../../../../../../../components/ModalWrapper/ModalWrapper';
import { Field, Form, FormElement } from '@progress/kendo-react-form';
import CInput from '../../../../../../../components/form-components/CInput';
import { isEmpty } from '../../../../../../../components/form-components/helpers/valodation-functions';
import { Button } from 'react-bootstrap';
import CTextArea from '../../../../../../../components/form-components/CTextArea';
import Parameters from '../components/Parameters';
import { useParams } from 'react-router-dom';
import { IConfParamOptions } from '../../../../../../../types/producVersionConfigurations';
import useConfigurations from '../../../../../../../helpers/hooks/useConfigurations';
import { getConfigParams, postConfig } from '../../../../../../../stores/productConfigurations/reducer';
import { useDispatch, useSelector } from 'react-redux';
import { IModalWrapperButton } from '../../../../../../../types/modal';
import { setPostReqResp } from '../../../../../../../stores/common/reducer';
import { IState } from '../../../../../../../stores/configureStore';
import { ConfigInformerModal } from './ConfigInformerModal';

const CreateConfigModal: FC<ICommonModalProps> = ({
  isOpen,
  onClose,
}) => {
  const dispatch = useDispatch()
  const headerText = 'Create configuration';
  const {configId} = useParams()
  const postResp = useSelector((state: IState) => state.common.postReqResp)
  const formSubmitBtnRef = useRef<HTMLButtonElement|null>(null)
  const formRef = useRef<Form>(null)
  const {configParams} = useConfigurations()
  const [formState, setFormState] = useState({})
  const [alertParams, setAlertParams] = useState({isOpen: false, isDataCashed: false, id:''})
  const modalButtons: IModalWrapperButton[] = [
    {buttonText: 'close', onButtonClick: () => onClose()},
    {buttonText: 'reset changes', onButtonClick: () => onResetPress()},
    {buttonText: 'save', onButtonClick: () => formSubmitBtnRef?.current?.click()}
  ]

  useEffect(()=>{
    if(isOpen){
      if(configId)
        dispatch(getConfigParams(configId))

      setFormState({
        name: '',
        comment: '',
        baseConfigurationId: configId,
        parameterValues: [],
      })
    }
  },[isOpen])
  useEffect(()=>{
    if(postResp === null ){
      setAlertParams({isOpen: true, isDataCashed: false, id:''})
      dispatch(setPostReqResp(''))
    }
    else if(postResp){
      setAlertParams({isOpen: true, isDataCashed: true, id:postResp})
      dispatch(setPostReqResp(''))
    }
  },[postResp])

  const onSubmitLocal = (formData: any) => {
    if(!formRef?.current?.isValid() || !configId) return

    const paramValues = Object.keys(formData?.parameterValues).map((key) => {
      const innerParam = formData?.parameterValues[key] as IConfParamOptions|string
      if(typeof innerParam === 'string')
        return {
          parameterDefinitionId: key,
          value: innerParam
        }
      return {
        parameterDefinitionId: innerParam?.parameterDefinitionId,
        value: innerParam?.value
      }
    })

    const data = {
      name: formData?.name,
      comment: formData?.comment,
      baseConfigurationId: configId,
      parameterValues: paramValues
    };

    dispatch(postConfig(data))
  }
  const onResetPress = () =>  dispatch(getConfigParams(configId??''))
  const onCloseConfigAlert = () =>  {
    setAlertParams({isOpen: false, isDataCashed: false, id: ''})
    onClose()
  }

  return (
    <>
      <ModalWrapper
        headerText={headerText}
        isOpen={isOpen}
        onClose={onClose}
        buttons={modalButtons}
      >
        <Form
          onSubmit={onSubmitLocal}
          initialValues={formState}
          key={JSON.stringify(formState)}
          ref={formRef}
          render={(formRenderProps) => (
            <FormElement className="create-config-form-modal">
              <fieldset className="k-form-fieldset">
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
                    label={"Comment"}
                    component={CTextArea}
                    max={200}
                    cols={25}
                    rows={2}
                    validator={isEmpty}
                  />
                </div>
                <div className="mb-3">
                  {
                    configParams &&
                    <Parameters configs={configParams} formRef={formRef} title="Please set configuration parameters:"/>
                  }
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
      <ConfigInformerModal
        isOpen={alertParams.isOpen}
        isDataCashed={alertParams.isDataCashed}
        cachedConfId={alertParams.id}
        onClose={onCloseConfigAlert}
      />
    </>
  )
}

export default CreateConfigModal