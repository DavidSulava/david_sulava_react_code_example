import { useDispatch, useSelector } from 'react-redux';
import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { getProdVersion, setProdVersion } from '../../../../stores/productVersion/reducer';
import useProdVersion from '../../../../helpers/hooks/storeHooks/useProdVersion';
import { Button } from 'react-bootstrap';
import { ICommonObject } from '../../../../types/common';
import ConfigurationsTable from './ConfigurationsTable/ConfigurationsTable';
import { IState } from '../../../../stores/configureStore';
import { getAppBundleList } from '../../../../stores/appBundle/reducer';
import { IAppBundle } from '../../../../types/appBundle';
import Spinner from '../../../../components/Loaders/Spinner/Spinner';

const buttonText = {
  Configurations: 'Configurations',
  InterfaceParameterDefinitions: 'InterfaceParameterDefinitions',
  UpdateVersion: 'Update version',
  CreatePublication: 'Create publication'
}
const ProdVersionPage = () => {
  const dispatch = useDispatch()
  const {versionId} = useParams();
  const {prodVersion, isProdVersionLoading} = useProdVersion()
  const appBundleList = useSelector((state: IState) => state.appBundle.appBundleList)
  const [defaultBundle, setDefaultBundle] = useState<IAppBundle|null>(null)
  const [btnPressed, setBtnPressed] = useState<ICommonObject>({
    [buttonText.Configurations]: false,
    [buttonText.InterfaceParameterDefinitions]: false,
    [buttonText.UpdateVersion]: false,
    [buttonText.CreatePublication]: false,
  })
  const NO_DATA_TEXT = 'N/A'

  useEffect(() => {
    dispatch(getAppBundleList())
    dispatch(getProdVersion(versionId ?? ''))
    setPressedBtn(buttonText.Configurations)
    return()=>{
      dispatch(setProdVersion(null))
    }
  }, [])
  useEffect(() => {
    if(appBundleList?.data.length){
      const presetBundle = appBundleList.data.find(item=> item.id === prodVersion?.appBundleId)
      const bundle = presetBundle||appBundleList?.data[0]
      setDefaultBundle(bundle)
    }
  }, [appBundleList, prodVersion])

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
        {
          isProdVersionLoading ?  <Spinner className="version-info-spinner"/> :
            <>
              <h4>{prodVersion?.name}</h4>
              <div className="version-info-body">
                <div className="info-column">
                  <div className='body-row'>
                    <span>Product name:</span>
                    <div>{prodVersion?.productName}</div>
                  </div>
                  <div className='body-row'>
                    <span>Product version:</span>
                    <div>{prodVersion?.version}</div>
                  </div>
                  <div className='body-row'>
                    <span>Design gear version:</span>
                    <div>{prodVersion?.designGearVersion}</div>
                  </div>
                  <div className='body-row'>
                    <span>Inventor version:</span>
                    <div>{prodVersion?.inventorVersion}</div>
                  </div>
                  <div className='body-row'>
                    <span>Description:</span>
                    <div> {NO_DATA_TEXT} </div>
                  </div>
                </div>
                <div className="info-column">
                  <div className='body-row'>
                    <span>AppBundle: </span>
                    <div>{defaultBundle?.name}</div>
                  </div>
                  <div className='body-row'>
                    <span>Author: </span>
                    <div> {NO_DATA_TEXT} </div>
                  </div>
                  <div className='body-row'>
                    <span>Company: </span>
                    <div> {NO_DATA_TEXT} </div>
                  </div>
                  <div className='body-row'>
                    <span>Date created:</span>
                    <div>{new Date(prodVersion?.created || '').toLocaleDateString()}</div>
                  </div>
                </div>
              </div>
            </>
        }

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