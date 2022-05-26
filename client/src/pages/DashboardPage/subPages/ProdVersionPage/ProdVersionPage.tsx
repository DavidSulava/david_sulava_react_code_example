import { useDispatch, useSelector } from 'react-redux';
import React, { MouseEventHandler, useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { getProdVersion } from '../../../../stores/productVersion/reducer';
import useProdVersion from '../../../../helpers/hooks/useProdVersion';
import { Button } from 'react-bootstrap';
import { ICommonObject } from '../../../../types/common';
import ConfigurationsTable from './ConfigurationsTable/ConfigurationsTable';
import { getAppBundle } from '../../../../stores/common/reducer';
import { IState } from '../../../../stores/configureStore';
import { DropDownList } from '@progress/kendo-react-dropdowns';

const buttonText = {
  Configurations: 'Configurations',
  InterfaceParameterDefinitions: 'InterfaceParameterDefinitions',
  UpdateVersion: 'Update version',
  CreatePublication: 'Create publication'
}
const ProdVersionPage = () => {
  const dispatch = useDispatch()
  const {versionId} = useParams();
  const {prodVersion} = useProdVersion()
  const appBundle = useSelector((state: IState) => state.common.appBundle)
  const [btnPressed, setBtnPressed] = useState<ICommonObject>({
    [buttonText.Configurations]: false,
    [buttonText.InterfaceParameterDefinitions]: false,
    [buttonText.UpdateVersion]: false,
    [buttonText.CreatePublication]: false,
  })

  useEffect(() => {
    dispatch(getAppBundle())
    dispatch(getProdVersion(versionId ?? ''))
    setPressedBtn(buttonText.Configurations)
  }, [])

  const setPressedBtn = (btn: string) => {
    const newState: ICommonObject = {}
    setBtnPressed(prev => {
      Object.entries(prev).forEach(([k, v], i) => {
        if(k === btn)
          return newState[k] = true
        newState[k] = false
      })
      return newState
    })
  }
  const onPressConfig = (e: React.MouseEvent) => {
    const target = e.target as HTMLElement
    setPressedBtn(target.innerText)

  }
  const onPressInterfaceParam = (e: React.MouseEvent) => {
    const target = e.target as HTMLElement
    setPressedBtn(target.innerText)
  }
  return (
    <div className="version-page">
      <div className="version-info">
        <h4>{prodVersion?.name}</h4>
        <div className="version-info-body">
          <div className="info-column">
            <div className='body-row'>
              <span>Product Version:</span>
              <div>{prodVersion?.version}</div>
            </div>
            <div className='body-row'>
              <span>Design Gear Version:</span>
              <div>{prodVersion?.designGearVersion}</div>
            </div>
            <div className='body-row'>
              <span>Inventor Version:</span>
              <div>{prodVersion?.inventorVersion}</div>
            </div>
            <div className='body-row'>
              <span>Date Created:</span>
              <div>{new Date(prodVersion?.created || '').toLocaleDateString()}</div>
            </div>
            <div className='body-row'>
              <span>Description:</span>
              <div> N/A </div>
            </div>
          </div>
          <div className="info-column">
            <div className='body-row'>
              <span>Product Properties</span>
            </div>
            <div className='body-row'>
              <span>Author: </span>
              <div> N/A </div>
            </div>
            <div className='body-row'>
              <span>Company: </span>
              <div> N/A </div>
            </div>
          </div>
          <div className="info-column">
            Select Your AppBundle
            <DropDownList
              name="AppBundleId"
              data={appBundle}
              textField="name"
              dataItemKey="id"
              defaultValue={appBundle[0]}
              // onChange={val => formRef?.current?.valueSetter("AppBundleId", val.target.value)}
              disabled={true}
            />
          </div>

        </div>
      </div>
      <div className="version-info-button-bar">
        <Button variant="outline-primary" onClick={onPressConfig} active={btnPressed.Configurations}>
          {buttonText.Configurations}
        </Button>
        <Button variant="outline-primary" onClick={onPressInterfaceParam} active={btnPressed.InterfaceParameterDefinitions} disabled={true}>
          {buttonText.InterfaceParameterDefinitions}
        </Button>
        <Button variant="outline-primary" disabled={true}>
          {buttonText.UpdateVersion}
        </Button>
        <Button variant="outline-primary" disabled={true}>
          {buttonText.CreatePublication}
        </Button>
      </div>
      <div className="version-info-table">
        {
          btnPressed[buttonText.Configurations] &&
          <ConfigurationsTable/>
        }
      </div>
    </div>
  )
}

export default ProdVersionPage