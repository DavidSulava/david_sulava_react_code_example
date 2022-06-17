import { Button } from 'react-bootstrap';
import React, { FC } from 'react';
import { IConfigItem, IParamItemPrepared, PARAM_CHANGED_CLASS_NAME } from './Parameters';
import { IConfigParam } from '../../../../../../../types/producVersionConfigurations';

interface IPanelProps {
  data: IConfigItem[],
  selectedNavBtn: string,
  className?: string,
  onNavClick: (id?: string) => void,
  parameterItems: IParamItemPrepared[]
}

const NavPanel: FC<IPanelProps> = ({data, selectedNavBtn, onNavClick, parameterItems,  className}) => {
  return (
    <>
      {
        data.map((item, index) => {
          const parameterItemInner = parameterItems.find(param => param.tempId === item.tempId)?.parameters.map(param => ({id: param.id, value: param.value}))
          const isChanged = !!item.parameters?.find(param => parameterItemInner?.find(parM=> parM.id === param.id && parM.value !== param.value))

          return (
            <div key={item?.tempId ?? '' + index} className={`${className}`}>
              <div className={`k-ml-${item.depth} k-mt-1`}>
                <Button
                  className={`${isChanged ? PARAM_CHANGED_CLASS_NAME : ''}`}
                  variant="outline-primary"
                  id={item.tempId}
                  onClick={() => onNavClick(item?.tempId)}
                  active={selectedNavBtn === item?.tempId}
                >
                  {item.value}
                </Button>
              </div>
              {
                !!item.children.length &&
                <NavPanel data={item.children} selectedNavBtn={selectedNavBtn} onNavClick={onNavClick} parameterItems={parameterItems} className={`k-ml-${item.depth}`}/>
              }
            </div>
          )
        })
      }
    </>
  )
}

export default NavPanel