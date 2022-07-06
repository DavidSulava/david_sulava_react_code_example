import React, { FC, useEffect, useRef, useState } from 'react';
import { Field, Form, FormElement } from '@progress/kendo-react-form';
import CInput from '../../../../../../components/form-components/CInput';
import { isEmpty } from '../../../../../../components/form-components/helpers/validation-functions';
import { Button } from 'react-bootstrap';
import { useParams } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import {
  getConfigParams,
  getConfigurationById,
  postConfig, putConfig,
  setConfiguration
} from '../../../../../../stores/productConfigurations/reducer';
import useConfigurations from '../../../../../../helpers/hooks/storeHooks/useConfigurations';
import Parameters from './components/Parameters';
import CTextArea from '../../../../../../components/form-components/CTextArea';
import BtnLink from '../../../../../../components/BtnLink';
import CreateConfigModal from './modals/CreateConfigModal';
import setPath from '../../../../../../helpers/setPath';
import { ERoutes } from '../../../../../../router/Routes';
import Spinner from '../../../../../../components/Loaders/Spinner/Spinner';
import { IConfigurationParamData, IPutConfigurations } from '../../../../../../types/producVersionConfigurations';
import FormCheckBox from '../../../../../../components/form-components/CheckBox';

const DefaultConfigForm: FC = () => {
  const dispatch = useDispatch()
  const {organizationId, productId, versionId, configId} = useParams()
  const {configParams, configuration, isConfigLoading} = useConfigurations()
  const formRef = useRef<Form>(null)
  const [formState, setFormState] = useState({})
  const [formConfigParams, setFormConfigParams] = useState<IConfigurationParamData>()
  const [isModalOpen, setIsModalOpen] = useState(false)

  useEffect(()=>{
    return ()=>{
      dispatch(setConfiguration(null))
    }
  },[])
  useEffect(() => {
    if(configId)
      dispatch(getConfigurationById(configId))
  }, [configId])
  useEffect(() => {
    if(configuration) {
      setFormState({
        name: configuration.name,
        comment: configuration.comment,
        baseConfigurationId: configuration.id,
        parameterValues: [],
        useAsTemplateConfiguration: configuration.useAsTemplateConfiguration
      })
      if(configParams)
        setFormConfigParams(configParams)
    }
  }, [configuration])

  const onClonePress = () => setIsModalOpen(!isModalOpen)
  const onSubmit = (formData: any) => {
    const data = {
      name: formData?.name,
      comment: formData?.comment,
      id: configId,
      useAsTemplateConfiguration: formData?.useAsTemplateConfiguration
    };
    dispatch(putConfig(data as IPutConfigurations))
  }

  return (
    <>
      <Form
        onSubmit={onSubmit}
        initialValues={formState}
        key={JSON.stringify(formState)}
        ref={formRef}
        render={(formRenderProps) => (
          <FormElement className="create-config-form-body">
            {
              isConfigLoading ? <Spinner/> :
                <fieldset className="k-form-fieldset">
                  <div className="mb-3">
                    <Field
                      name={"name"}
                      label={"Name"}
                      component={CInput}
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
                      // validator={isEmpty}
                    />
                  </div>
                  <div className="mb-3">
                    <Field
                      name="useAsTemplateConfiguration"
                      id="useAsTemplateConfiguration"
                      component={FormCheckBox}
                      label="set as template configuration"
                    />
                  </div>
                  <div className="mb-3">
                    {
                      <Parameters
                        title="Configuration parameters:"
                        formRef={formRef}
                        configs={formConfigParams}
                        isDisabled={true}
                      />
                    }

                  </div>
                </fieldset>
            }

            <div className="prod-config-button-bar">
              <BtnLink
                to={setPath(ERoutes.ProdVersion, [organizationId, productId, versionId])}
                className='btn btn-outline-primary'
              >
                close
              </BtnLink>
              <Button type="submit">save</Button>
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