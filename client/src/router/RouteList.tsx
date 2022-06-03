import { Route, Routes } from 'react-router-dom';
import { ERoutes } from './Routes';
import ProtectedRoutes from './components/protected-routes';
import LoginPage from '../pages/AuthPages/LoginPage/LoginPage';
import OrganisationPage from '../pages/OrganisationPage/OrganisationPage';
import React from 'react';
import GuestRoutes from './components/guest-routes';
import DashboardPage from '../pages/DashboardPage/DashboardPage';
import StatisticsPage from '../pages/DashboardPage/subPages/StatisticsPage';
import ManageOrganisationPage from '../pages/DashboardPage/subPages/ManageOrganisationPage';
import ProductsPage from '../pages/DashboardPage/subPages/ProductsPage/ProductsPage';
import PublicationsPage from '../pages/DashboardPage/subPages/PublicationsPage';
import CPThresholdPage from '../pages/DashboardPage/subPages/CPThresholdPage';
import UsersPage from '../pages/DashboardPage/subPages/UsersPage';
import BillingPage from '../pages/DashboardPage/subPages/BillingPage';
import RegistrationPage from '../pages/AuthPages/RegistrationPage/RegistartionPage';
import ForgotPassword from '../pages/AuthPages/ForgotPassword/ForgotPassword';
import VersionsPage from '../pages/DashboardPage/subPages/ProdVersionsPage/VersionsPage';
import ProdVersionPage from '../pages/DashboardPage/subPages/ProdVersionPage/ProdVersionPage';

const RouteList = () => {
  return (
    <Routes>
      {/*Guest Routes*/}
      <Route element={<GuestRoutes/>}>
        <Route index element={<LoginPage/>}/>
        <Route path={ERoutes.SignUp} element={<RegistrationPage/>}/>
        <Route path={ERoutes.ForgotPwd} element={<ForgotPassword/>}/>
      </Route>

      {/*Protected Routes*/}
      <Route element={<ProtectedRoutes isAllowed={true}/>}>
        <Route path={ERoutes.Organisations} element={<OrganisationPage/>}/>
        <Route path={ERoutes.Dashboard} element={<DashboardPage/>}>
          <Route index element={<StatisticsPage/>}/>
          <Route path={ERoutes.ManageOrganisation} element={<ManageOrganisationPage/>}/>
          <Route path={ERoutes.Products} element={<ProductsPage/>}/>
          <Route path={ERoutes.ProdVersions} element={<VersionsPage/>}/>
          <Route path={ERoutes.ProdVersion} element={<ProdVersionPage/>}/>
          <Route path={ERoutes.Publications} element={<PublicationsPage/>}/>
          <Route path={ERoutes.CPThreshold} element={<CPThresholdPage/>}/>
          <Route path={ERoutes.Users} element={<UsersPage/>}/>
          <Route path={ERoutes.Billing} element={<BillingPage/>}/>
        </Route>
      </Route>
    </Routes>
  )
}

export default RouteList;