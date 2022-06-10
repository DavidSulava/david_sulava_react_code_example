import { Route, Routes } from 'react-router-dom';
import { ERoutes } from './Routes';
import ProtectedRoutes from './components/protected-routes';
import LoginPage from '../pages/AuthPages/LoginPage/LoginPage';
import OrganisationPage from '../pages/OrganisationPage/OrganisationPage';
import React from 'react';
import GuestRoutes from './components/guest-routes';
import DashboardPage from '../pages/DashboardPage/DashboardPage';
import StatisticsPage from '../pages/DashboardPage/pages/StatisticsPage';
import ManageOrganisationPage from '../pages/DashboardPage/pages/ManageOrganisationPage';
import ProductsPage from '../pages/DashboardPage/pages/ProductsPage/ProductsPage';
import PublicationsPage from '../pages/DashboardPage/pages/PublicationsPage';
import CPThresholdPage from '../pages/DashboardPage/pages/CPThresholdPage';
import UsersPage from '../pages/DashboardPage/pages/UsersPage';
import BillingPage from '../pages/DashboardPage/pages/BillingPage';
import RegistrationPage from '../pages/AuthPages/RegistrationPage/RegistartionPage';
import ForgotPassword from '../pages/AuthPages/ForgotPassword/ForgotPassword';
import VersionsPage from '../pages/DashboardPage/pages/ProdVersionsPage/VersionsPage';
import ProdVersionPage from '../pages/DashboardPage/pages/ProdVersionPage/ProdVersionPage';
import ProductConfigurationPage from '../pages/DashboardPage/pages/ProductConfigurationPage/ProductConfigurationPage';
import ManageAccountPage from '../pages/ManageAccountPage/ManageAccountPage';
import InfoDetailsPage from '../pages/ManageAccountPage/pages/InfoDetailsPage/InfoDetailsPage';
import SecurityPage from '../pages/ManageAccountPage/pages/SecurityPage/SecurityPage';
import ResetPasswordPage from '../pages/AuthPages/ResetPasswordPage/ResetPasswordPage';

const RouteList = () => {
  return (
    <Routes>
      {/*Guest Routes*/}
      <Route element={<GuestRoutes/>}>
        <Route index element={<LoginPage/>}/>
        <Route path={ERoutes.SignUp} element={<RegistrationPage/>}/>
        <Route path={ERoutes.ForgotPwd} element={<ForgotPassword/>}/>
        <Route path={ERoutes.ResetPwd} element={<ResetPasswordPage/>}/>
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
        <Route path={ERoutes.ProdVersionConfig} element={<ProductConfigurationPage/>}/>

        <Route path={ERoutes.ManageAccount} element={<ManageAccountPage/>}>
          <Route index element={<InfoDetailsPage/>}/>
          <Route path={ERoutes.AccountSecurity} element={<SecurityPage/>}/>
        </Route>
      </Route>
    </Routes>
  )
}

export default RouteList;