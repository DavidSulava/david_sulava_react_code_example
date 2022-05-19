import { Link, Outlet, NavLink, useLocation } from 'react-router-dom';
import { ERoutes } from '../../router/Routes';
import { Button } from 'react-bootstrap';
import React from 'react';
import useAuthCheck from '../../helpers/hooks/useAuthCheck';

const DashboardPage = () => {
  const location = useLocation();
  const {user} = useAuthCheck()

  const isNavLikActive = (route: string): boolean => {
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
          <Button variant="outlined" className="btn-outline-info">
            <Link to={ERoutes.Root}>Return</Link>
          </Button>
        </div>

      </div>
      <div className="dash-board-body">
        <div className="dash-board-navigation">
          <Button variant="outlined" className="btn-outline-info" active={isNavLikActive(ERoutes.Dashboard)}>
            <NavLink to={ERoutes.Dashboard} className="nav-link ">Statistics</NavLink>
          </Button>
          <Button variant="outlined" className="btn-outline-info" active={isNavLikActive(ERoutes.ManageOrganisation)}>
            <Link to={ERoutes.ManageOrganisation} className="nav-link">Manage organisation</Link>
          </Button>
          <Button variant="outlined" className="btn-outline-info" active={isNavLikActive(ERoutes.Products)}>
            <Link to={ERoutes.Products} className="nav-link">Products</Link>
          </Button>
          <Button variant="outlined" className="btn-outline-info" active={isNavLikActive(ERoutes.Publications)}>
            <Link to={ERoutes.Publications} className="nav-link">Publications</Link>
          </Button>
          <Button variant="outlined" className="btn-outline-info" active={isNavLikActive(ERoutes.CPThreshold)}>
            <Link to={ERoutes.CPThreshold} className="nav-link">CP Threshold</Link>
          </Button>
          <Button variant="outlined" className="btn-outline-info" active={isNavLikActive(ERoutes.Users)}>
            <Link to={ERoutes.Users} className="nav-link">Users</Link>
          </Button>
          <Button variant="outlined" className="btn-outline-info" active={isNavLikActive(ERoutes.Billing)}>
            <Link to={ERoutes.Billing} className="nav-link">Billing</Link>
          </Button>
        </div>

        <div className="dash-board-content">
          <Outlet/>
        </div>
      </div>
    </>
  )
}

export default DashboardPage