import { Navigate, Outlet, RouteProps, useLocation } from 'react-router-dom';
import React from 'react';
import { ERoutes } from '../Routes';
import useAuthCheck from '../../helpers/hooks/useAuthCheck';

export interface controlledRouteProps extends RouteProps {
  isAllowed?: boolean,
  redirectPath?: string,
  children?: React.ReactElement
}

const ProtectedRoutes: React.FC<controlledRouteProps> = ({
  isAllowed,
  redirectPath = ERoutes.Root,
  children,
}) => {
  const {authenticated, authLoading} = useAuthCheck()
  const location = useLocation();

  if(!authenticated && !authLoading) {
    return <Navigate to={redirectPath} state={{from: location}} replace/>;
  }

  return (
    children ? children : <Outlet/>
  )
};

export default ProtectedRoutes