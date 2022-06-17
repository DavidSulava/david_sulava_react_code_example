import { IConfigParam, IConfigurationParamData } from '../../../../../../../types/producVersionConfigurations';
import React, { FC, RefObject, useEffect, useState } from 'react';
import { Field, Form } from '@progress/kendo-react-form';
import CInput from '../../../../../../../components/form-components/CInput';
import { isEmpty } from '../../../../../../../components/form-components/helpers/validation-functions';
import { DropDownList, DropDownListChangeEvent } from '@progress/kendo-react-dropdowns';
import NavPanel from './NavPannelParams';
import Spinner from '../../../../../../../components/Loaders/Spinner/Spinner';
import { Nullable } from '../../../../../../../types/common';

export const PARAM_CHANGED_CLASS_NAME = 'item-changed'

export interface IParamItemPrepared {
  tempId: string,
  parameters: IConfigParam[]
}

export interface IConfigItem {
  value: string,
  id: string,
  children: IConfigItem[],
  depth: number,
  parentId?: string,
  tempId?: string,
  parameters?: IConfigParam[]
}

interface IParamProps {
  configs: Nullable<IConfigurationParamData>
  formRef: RefObject<Form>
  title: string,
  isDisabled?: boolean,
  isLoading?: boolean,
}

const Parameters: FC<IParamProps> = ({
  configs,
  formRef,
  title,
  isDisabled = false,
  isLoading= false
}) => {
  const ARRAY_INPUT_NAME_C_PARAMS = 'parameterValues'
  const [configItems, setConfigItems] = useState<IConfigItem>()
  const [parameterItems, setParameterItems] = useState<IParamItemPrepared[]>([])
  const [defaultParameterItems, setDefaultParameterItems] = useState<IConfigParam[]>([])
  const [selectedNavBtn, setSelectedNavBtn] = useState('')
  const [selectedParams, setSelectedParams] = useState<IConfigParam[]>([])

  useEffect(() => {
    formRef?.current?.valueSetter('parameterValues', '')
    setSelectedParams([])
    if(configs){
      const {configItems, paramList} = getItemsRecursive(configs)
      setConfigItems(configItems)
      setParameterItems(paramList)
      setDefaultParameterItems(paramList.map(param => param.parameters).flat())
      setSelectedNavItem(configItems?.tempId)
    }
  }, [configs])
  useEffect(() => {
    if(selectedNavBtn) {
      const parameters = parameterItems.find((param => param.tempId === selectedNavBtn))
      const sortedParams = [...parameters?.parameters ?? []].sort((A, B) => B?.displayPriority - A?.displayPriority)
      setSelectedParams(sortedParams)
    }
  }, [selectedNavBtn])
  useEffect(() => {
    setFormDataForParameters(parameterItems)
  }, [parameterItems])

  const getItemsRecursive = (configsParam: IConfigurationParamData, paramList: IParamItemPrepared[] = [], depth: number = 0) => {
    const innerDepth = depth
    const randomId = Math.random().toString(36).slice(2, 7);
    const innerConfig: IConfigItem = {
      value: configsParam.componentName,
      id: configsParam.componentId,
      depth: innerDepth,
      tempId: randomId,
      children: [],
      parameters: configsParam.parameters
    }
    const innerParamList: IParamItemPrepared[] = paramList
    innerParamList.push({tempId: randomId, parameters: configsParam.parameters})

    configsParam.childs.forEach((item, childIndex) => {
      const innerRandomId = Math.random().toString(36).slice(2, 7);
      const itemInnerConfig = item.childs.length ? getItemsRecursive(item, innerParamList, innerDepth + 1).configItems.children : []
      const preparedItemConf = {
        value: item.componentName,
        id: item.componentId,
        depth: innerDepth + 1,
        tempId: innerRandomId,
        children: itemInnerConfig,
        parameters: item.parameters
      }
      innerConfig.children.push(preparedItemConf as IConfigItem)
      innerParamList.push({tempId: innerRandomId, parameters: item.parameters})
    })
    return {configItems: innerConfig, paramList: innerParamList}
  }
  const getDefaultValOptions = (item: IConfigParam) => {
    return item.valueOptions.find(option => option.value === item.value)
  }
  const setFormDataForParameters = (paramList: IParamItemPrepared[]) => {
    formRef?.current?.valueSetter(ARRAY_INPUT_NAME_C_PARAMS, '')
    paramList.forEach((item, index) => {
      item?.parameters.forEach((param, index) => {
        if(!param?.valueOptions) {
          formRef?.current?.valueSetter(ARRAY_INPUT_NAME_C_PARAMS + '.' + param.id, param.value)
          return
        }

        const defaultParam = getDefaultValOptions(param)
        if(defaultParam)
          formRef?.current?.valueSetter(ARRAY_INPUT_NAME_C_PARAMS + '.' + defaultParam.id, defaultParam)
      })
    })
  }
  const setSelectedNavItem = (id?: string) => setSelectedNavBtn(id ?? '')
  const isParamChanged = (param:IConfigParam) => defaultParameterItems.find(defItem => defItem.id === param.id)?.value !== param.value
  const onParamChange = (e: DropDownListChangeEvent, param: IConfigParam) => {
    const chosenValue = e.value?.value

    setSelectedParams(prev => {
      return prev.map(item => {
        if(item.id === param?.id)
          return {...item, value: chosenValue}
        return item
      })
    })
    setParameterItems(prev => {
      return prev.map(item => {
        if(item.tempId === selectedNavBtn) {
          const updatedItems = item.parameters.map(paramItem => {
            if(paramItem.id === param.id)
              return {...paramItem, value: chosenValue}
            return paramItem
          })
          return {...item, parameters: updatedItems}
        }
        return item
      })
    })
  }

  return (
    <>
      <legend className="k-form-legend">
        {title}
      </legend>
      <div className="config-parameters-container">
        {
          isLoading ? <Spinner/> :
            <>
              <div className="conf-param-navigation">
                {
                  configItems &&
                  <NavPanel data={[configItems]} selectedNavBtn={selectedNavBtn} onNavClick={setSelectedNavItem} parameterItems={parameterItems}/>
                }
              </div>
              <div className="conf-param-form-fields">
                {
                  selectedParams.map((param, index) => {
                    const fieldName = param.valueOptions ? ARRAY_INPUT_NAME_C_PARAMS + '.' + getDefaultValOptions(param)?.id || param.id : ARRAY_INPUT_NAME_C_PARAMS + '.' + param.id
                    const isChanged = isParamChanged(param)

                    return (
                      <div key={param.id} className={`param-field-container ${isChanged ? PARAM_CHANGED_CLASS_NAME : ''}`}>
                        {
                          !param.isHidden &&
                          (
                            param.valueOptions ?
                              <div>
                                <div>{param.displayName}</div>
                                <DropDownList
                                  name={fieldName}
                                  data={param.valueOptions}
                                  textField="value"
                                  dataItemKey="id"
                                  defaultValue={getDefaultValOptions(param)}
                                  onChange={(e) => onParamChange(e, param)}
                                  required={true}
                                  disabled={param.isReadOnly || isDisabled}
                                />
                              </div>
                              :
                              <Field
                                wrapperClassName={!index && 'mt-0'}
                                name={fieldName}
                                component={CInput}
                                label={param.displayName}
                                validator={isEmpty}
                                onChange={val => formRef?.current?.valueSetter(fieldName, val.target.value)}
                                disabled={param.isReadOnly}
                              />
                          )
                        }
                      </div>
                    )
                  })
                }
              </div>
            </>
        }

      </div>
    </>
  )
}

export default Parameters