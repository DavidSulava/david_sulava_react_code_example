import { Navigate, Outlet, RouteProps, useLocation } from 'react-router-dom';
import React from 'react';
import { ERoutes } from '../Routes';
import useAuthCheck from '../../helpers/hooks/useAuthCheck';

export interface propsGuest extends RouteProps {
  isAllowed?: boolean,
  redirectPath?: string,
  children?: React.ReactElement
}

const GuestRoutes: React.FC<propsGuest> = ({
  redirectPath = ERoutes.Organisations,
  children,
}) => {
  const {authenticated, checkingAuth} = useAuthCheck()
  const location = useLocation();

  if(authenticated && !checkingAuth) {
    return <Navigate to={redirectPath} state={{from: location}} replace/>;
  }

  return (
    children ? children : <Outlet/>
  )
};

export default GuestRoutes