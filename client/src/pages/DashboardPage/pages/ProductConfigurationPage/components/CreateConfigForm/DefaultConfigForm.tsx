import React, { FC, useEffect, useRef, useState } from 'react';
import { Field, Form, FormElement } from '@progress/kendo-react-form';
import CInput from '../../../../../../components/form-components/CInput';
import { isEmpty } from '../../../../../../components/form-components/helpers/valodation-functions';
import { Button } from 'react-bootstrap';
import { useParams } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { getConfigParams, getConfigurationById } from '../../../../../../stores/productConfigurations/reducer';
import useConfigurations from '../../../../../../helpers/hooks/useConfigurations';
import Parameters from './components/Parameters';
import CTextArea from '../../../../../../components/form-components/CTextArea';
import BtnLink from '../../../../../../components/BtnLink';
import CreateConfigModal from './modals/CreateConfigModal';
import setPath from '../../../../../../helpers/setPath';
import { ERoutes } from '../../../../../../router/Routes';


const DefaultConfigForm: FC = () => {
  const dispatch = useDispatch()
  const {organizationId, productId, versionId, configId} = useParams()
  const {configParams, configuration} = useConfigurations()
  const formRef = useRef<Form>(null)
  const [formState, setFormState] = useState({})
  const [isModalOpen, setIsModalOpen] = useState(false)


  useEffect(()=>{
    if(configId){
      dispatch(getConfigurationById(configId))
      dispatch(getConfigParams(configId))
    }

  },[configId])
  useEffect(()=>{
    if(configuration){
      setFormState({
        name: configuration.name,
        comment: configuration.comment,
        baseConfigurationId: configuration.id,
        parameterValues: [],
      })
    }
  },[configuration])

  const onClonePress = () => setIsModalOpen(!isModalOpen)

  return (
    <>
      <Form
        initialValues={formState}
        key={JSON.stringify(formState)}
        ref={formRef}
        render={(formRenderProps) => (
          <FormElement className="create-config-form-body">
            <fieldset className="k-form-fieldset">
              <div className="mb-3">
                <Field
                  name={"name"}
                  label={"Name"}
                  component={CInput}
                  disabled={true}
                  validator={isEmpty}
                />
              </div>
              <div className="mb-3">
                <Field
                  name={"comment"}
                  label={"Comment"}
                  component={CTextArea}
                  disabled={true}
                  max={200}
                  cols={25}
                  rows={2}
                  validator={isEmpty}
                />
              </div>
              <div className="mb-3">
                {
                  configParams &&
                  <Parameters configs={configParams} formRef={formRef} title="Configuration parameters:" isDisabled={true}/>
                }
              </div>
            </fieldset>

            <div className="prod-config-button-bar">
              <BtnLink to={setPath(ERoutes.ProdVersion, [organizationId, productId, versionId])} className='btn btn-outline-primary'>close</BtnLink>
              <Button onClick={onClonePress}>clone</Button>
            </div>
          </FormElement>
        )}
      />
      <CreateConfigModal isOpen={isModalOpen} onClose={onClonePress}/>
    </>

  )
}

export default DefaultConfigForm