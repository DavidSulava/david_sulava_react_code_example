import { Outlet, useLocation, useParams } from 'react-router-dom';
import { ERoutes } from '../../router/Routes';
import React, { useEffect } from 'react';
import useAuthCheck from '../../helpers/hooks/useAuthCheck';
import setPath from '../../helpers/setPath';
import BtnLink from '../../components/BtnLink';
import { authOrg } from '../../stores/authentication/reducer';
import { useDispatch } from 'react-redux';
import { initialProdVerState, setProdVersionDataState } from '../../stores/productVersion/reducer';

const DashboardPage = () => {
  const dispatch = useDispatch()
  const location = useLocation();
  const {organizationId} = useParams();
  const {user} = useAuthCheck()

  const dashboardRoot = ERoutes.Dashboard.match(/(?!\/).*(?=\/)/)?.[0]
  const insideSubPageReg = new RegExp(`(${dashboardRoot}\/)([A-z\\d\\-]+\/){2,}`)

  useEffect(() => {
    dispatch(authOrg(organizationId ?? ''))
    return () => {
      dispatch(setProdVersionDataState(initialProdVerState.dataState))
    }
  }, [dispatch])
  const isNavLikActive = (route: string = ""): boolean => {
    return location.pathname.endsWith(route)
  }

  return (
    <>
      <div className="dash-board-header">
        <div>
          <h6>Welcome, {user?.firstName}</h6>
          <h6>Your id: {user?.id}</h6>
        </div>
        <div>
          {
            location.pathname.match(insideSubPageReg) &&
            <BtnLink to={-1 as any} className='btn btn-outline-primary'>Return </BtnLink>
          }
        </div>

      </div>
      <div className="dash-board-body">
        <div className="dash-board-navigation">
          <BtnLink
            to={setPath(ERoutes.Dashboard, [organizationId])}
            isActive={isNavLikActive(organizationId)}
            className='btn btn-outline-primary mb-2'
          >
            Statistics
          </BtnLink>
          <BtnLink
            to={setPath(ERoutes.ManageOrganisation, [organizationId])}
            isActive={isNavLikActive(ERoutes.ManageOrganisation)}
            className='btn btn-outline-primary mb-2'
          >
            Manage organisation
          </BtnLink>
          <BtnLink
            to={setPath(ERoutes.Products, [organizationId])}
            isActive={isNavLikActive(ERoutes.Products)}
            className='btn btn-outline-primary mb-2'
          >
            Products
          </BtnLink>
          <BtnLink
            to={setPath(ERoutes.Publications, [organizationId])}
            isActive={isNavLikActive(ERoutes.Publications)}
            className='btn btn-outline-primary mb-2'
          >
            Publications
          </BtnLink>
          <BtnLink
            to={setPath(ERoutes.CPThreshold, [organizationId])}
            isActive={isNavLikActive(ERoutes.CPThreshold)}
            className='btn btn-outline-primary mb-2'
          >
            CP Threshold
          </BtnLink>
          <BtnLink
            to={setPath(ERoutes.Users, [organizationId])}
            isActive={isNavLikActive(ERoutes.Users)}
            className='btn btn-outline-primary mb-2'
          >
            Users
          </BtnLink>
          <BtnLink
            to={setPath(ERoutes.Billing, [organizationId])}
            isActive={isNavLikActive(ERoutes.Billing)}
            className='btn btn-outline-primary mb-2'
          >
            Billing
          </BtnLink>
        </div>

        <div className="dash-board-content">
          <Outlet/>
        </div>
      </div>
    </>
  )
}

export default DashboardPage