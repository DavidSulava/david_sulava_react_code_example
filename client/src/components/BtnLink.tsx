import { Link } from 'react-router-dom';
import React, { FC, PropsWithChildren } from 'react';
import { IProduct } from '../types/product';

interface IBtnLinkProps {
  idDisabled?: boolean,
  isActive?: boolean,
  to: string,
  className?: string,
  state?: {}
  [key:string]: any
}

const BtnLink: FC<PropsWithChildren<IBtnLinkProps>> = ({
  idDisabled = false,
  isActive = false,
  className = '',
  to,
  state,
  children,
  ...others
}) => {
  const isDisabledInner = idDisabled ? 'disabled' : ''
  const isActiveInner = isActive ? 'active' : ''
  const classNameInner = className ? className : ''

  return (
    <Link role="button" className={`${classNameInner} ${isDisabledInner} ${isActiveInner}`} to={to} state={{...state}}  {...others}>
      {children}
    </Link>
  )
}

export default BtnLink