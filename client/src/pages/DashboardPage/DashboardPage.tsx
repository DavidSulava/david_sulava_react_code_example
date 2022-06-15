import { Outlet, useLocation, useParams } from 'react-router-dom';
import { ERoutes } from '../../router/Routes';
import React, { useEffect } from 'react';
import useAuthCheck from '../../helpers/hooks/useAuthCheck';
import setPath from '../../helpers/setPath';
import BtnLink from '../../components/BtnLink';
import { useDispatch } from 'react-redux';
import { initialProdVerState, setProdVersionDataState } from '../../stores/productVersion/reducer';
import { isNavLikActive } from '../../helpers/navLink';

const DashboardPage = () => {
  const dispatch = useDispatch()
  const location = useLocation();
  const {organizationId, productId} = useParams();
  const {user} = useAuthCheck()

  const dashboardRootRegExp = ERoutes.Dashboard.match(/(?!\/).*(?=\/)/)?.[0]
  const insideSubPageRegExp = new RegExp(`(${dashboardRootRegExp}\/)([A-z\\d\\-]+\/){2,}`)
  const onProductVersionListPageRegExp = new RegExp(`(${dashboardRootRegExp}\/)([A-z\\d\\-]+\/){2}products`)
  const onProductVersionPageRegExp = new RegExp(`(${dashboardRootRegExp}\/)([A-z\\d\\-]+\/){3}products`)

  useEffect(() => {
    return () => {
      dispatch(setProdVersionDataState(initialProdVerState.dataState))
    }
  }, [dispatch])

  const returnBack = () => {
    if(location.pathname.match(onProductVersionListPageRegExp))
      return setPath(ERoutes.Products, [organizationId])
    else if(location.pathname.match(onProductVersionPageRegExp) && productId)
      return setPath(ERoutes.ProdVersions, [organizationId, productId])

    return -1 as any
  }

  return (
    <div className="page-body">
      <div className="dash-board-header">
        <div>
          <h6>Welcome, {user?.firstName}</h6>
          <h6>Your id: {user?.id}</h6>
        </div>
        <div>
          {
            location.pathname.match(insideSubPageRegExp) &&
            <BtnLink to={returnBack()} className='btn btn-outline-primary'>Return</BtnLink>
          }
        </div>
      </div>
      <div className="dash-board-body">
        <div className="dash-board-navigation">
          <BtnLink
            to={setPath(ERoutes.Dashboard, [organizationId])}
            isActive={isNavLikActive(organizationId, location.pathname)}
            className='btn btn-outline-primary mb-2'
          >
            Statistics
          </BtnLink>
          <BtnLink
            to={setPath(ERoutes.ManageOrganisation, [organizationId])}
            isActive={isNavLikActive(ERoutes.ManageOrganisation, location.pathname)}
            className='btn btn-outline-primary mb-2'
          >
            Manage organisation
          </BtnLink>
          <BtnLink
            to={setPath(ERoutes.Products, [organizationId])}
            isActive={isNavLikActive(ERoutes.Products, location.pathname)}
            className='btn btn-outline-primary mb-2'
          >
            Products
          </BtnLink>
          <BtnLink
            to={setPath(ERoutes.Publications, [organizationId])}
            isActive={isNavLikActive(ERoutes.Publications, location.pathname)}
            className='btn btn-outline-primary mb-2'
          >
            Publications
          </BtnLink>
          <BtnLink
            to={setPath(ERoutes.CPThreshold, [organizationId])}
            isActive={isNavLikActive(ERoutes.CPThreshold, location.pathname)}
            className='btn btn-outline-primary mb-2'
          >
            CP Threshold
          </BtnLink>
          <BtnLink
            to={setPath(ERoutes.Users, [organizationId])}
            isActive={isNavLikActive(ERoutes.Users, location.pathname)}
            className='btn btn-outline-primary mb-2'
          >
            Users
          </BtnLink>
          <BtnLink
            to={setPath(ERoutes.Billing, [organizationId])}
            isActive={isNavLikActive(ERoutes.Billing, location.pathname)}
            className='btn btn-outline-primary mb-2'
          >
            Billing
          </BtnLink>
        </div>

        <div className="dash-board-content">
          <Outlet/>
        </div>
      </div>
    </div>
  )
}

export default DashboardPage