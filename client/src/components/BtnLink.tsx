import { Link } from 'react-router-dom';
import React, { FC, PropsWithChildren } from 'react';

interface IBtnLinkProps {
  idDisabled?: boolean,
  isActive?: boolean,
  to: string,
  className?: string
}

const BtnLink: FC<PropsWithChildren<IBtnLinkProps>> = ({idDisabled=false, isActive= false, className='', to, children}) => {
  const isDisabledInner = idDisabled ? 'disabled' : ''
  const isActiveInner = isActive ? 'active' : ''
  const classNameInner = className? className: 'btn-outline-primary'

  return (
    <Link role="button" className={`btn ${classNameInner} ${isDisabledInner} ${isActiveInner}`} to={to}>
      {children}
    </Link>
  )
}

export default BtnLink