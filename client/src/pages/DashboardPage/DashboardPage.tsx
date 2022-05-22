import { Link, Outlet, NavLink, useLocation, useParams } from 'react-router-dom';
import { ERoutes } from '../../router/Routes';
import { Button } from 'react-bootstrap';
import React from 'react';
import useAuthCheck from '../../helpers/hooks/useAuthCheck';
import setPath from '../../helpers/setPath';
import BtnLink from '../../components/BtnLink';

const DashboardPage = () => {
  const location = useLocation();
  const { organizationId } = useParams();
  const {user} = useAuthCheck()

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
          {/*<Button variant="outlined" className="btn-outline-info btn-nav">*/}
          {/*  <Link to={ERoutes.Root}>Return</Link>*/}
          {/*</Button>*/}
          <BtnLink to={ERoutes.Root} className='btn btn-outline-primary'>Return</BtnLink>
        </div>

      </div>
      <div className="dash-board-body">
        <div className="dash-board-navigation">
          <BtnLink to={setPath(ERoutes.Dashboard, [organizationId])} isActive={isNavLikActive(organizationId)} className='btn btn-outline-primary mb-2'>Statistics</BtnLink>
          <BtnLink to={setPath(ERoutes.ManageOrganisation, [organizationId])} isActive={isNavLikActive(ERoutes.ManageOrganisation)} className='btn btn-outline-primary mb-2'>Manage organisation</BtnLink>
          <BtnLink to={setPath(ERoutes.Products, [organizationId])} isActive={isNavLikActive(ERoutes.Products)} className='btn btn-outline-primary mb-2'>Products</BtnLink>
          <BtnLink to={setPath(ERoutes.Publications, [organizationId])} isActive={isNavLikActive(ERoutes.Publications)} className='btn btn-outline-primary mb-2'>Publications</BtnLink>
          <BtnLink to={setPath(ERoutes.CPThreshold, [organizationId])} isActive={isNavLikActive(ERoutes.CPThreshold)} className='btn btn-outline-primary mb-2'>CP Threshold</BtnLink>
          <BtnLink to={setPath(ERoutes.Users, [organizationId])} isActive={isNavLikActive(ERoutes.Users)} className='btn btn-outline-primary mb-2'>Users</BtnLink>
          <BtnLink to={setPath(ERoutes.Billing, [organizationId])} isActive={isNavLikActive(ERoutes.Billing)} className='btn btn-outline-primary mb-2'>Billing</BtnLink>
        </div>

        <div className="dash-board-content">
          <Outlet/>
        </div>
      </div>
    </>
  )
}

export default DashboardPage