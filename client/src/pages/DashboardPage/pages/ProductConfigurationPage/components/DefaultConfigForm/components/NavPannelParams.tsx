import { Button } from 'react-bootstrap';
import React, { FC } from 'react';
import { IConfigItem } from './Parameters';

interface IPanelProps {
  data: IConfigItem[],
  selectedNavBtn: string,
  className?: string,
  onNavClick: (id?: string) => void,
}

const NavPanel: FC<IPanelProps> = ({data, selectedNavBtn, onNavClick, className}) => {
  return (
    <>
      {
        data.map((item, index) => {
          return (
            <div key={item?.tempId ?? '' + index} className={`${className}`}>
              <div className={`k-ml-${item.depth} k-mt-1`}>
                <Button
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
                <NavPanel data={item.children} selectedNavBtn={selectedNavBtn} onNavClick={onNavClick} className={`k-ml-${item.depth}`}/>
              }
            </div>
          )
        })
      }
    </>
  )
}

export default NavPanel