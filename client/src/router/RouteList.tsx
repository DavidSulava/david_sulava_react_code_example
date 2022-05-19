import { Navigate, Route, Routes } from 'react-router-dom';
import { ERoutes } from './Routes';
import ProtectedRoutes from './components/protected-routes';
import LoginPage from '../pages/LoginPage/LoginPage';
import OrganisationPage from '../pages/OrganisationPage/OrganisationPage';
import React from 'react';
import GuestRoutes from './components/guest-routes';
import DashboardPage from '../pages/DashboardPage/DashboardPage';
import Statistics from '../pages/DashboardPage/components/Statistics';
import ManageOrganisation from '../pages/DashboardPage/components/ManageOrganisation';
import Products from '../pages/DashboardPage/components/Products';
import Publications from '../pages/DashboardPage/components/Publications';
import CPThreshold from '../pages/DashboardPage/components/CPThreshold';
import Users from '../pages/DashboardPage/components/Users';
import Billing from '../pages/DashboardPage/components/Billing';
import RegistrationPage from '../pages/RegistrationPage/RegistartionPage';
import ForgotPassword from '../pages/ForgotPassword/ForgotPassword';

const RouteList = () => {
  return (
    <Routes>
      {/*Guest Routes*/}
      <Route element={<GuestRoutes/>}>
        <Route index element={<LoginPage/>}/>
        <Route path={ERoutes.SignUp}  element={<RegistrationPage/>}/>
        <Route path={ERoutes.ForgotPwd}  element={<ForgotPassword/>}/>
      </Route>

      {/*Protected Routes*/}
      <Route element={<ProtectedRoutes isAllowed={true}/>}>
        <Route path={ERoutes.Organisations} element={<OrganisationPage/>}/>
        <Route path={ERoutes.Dashboard} element={<DashboardPage/>}>
          <Route index element={<Statistics/>}/>
          <Route path={ERoutes.ManageOrganisation} element={<ManageOrganisation/>}/>
          <Route path={ERoutes.Products} element={<Products/>}/>
          <Route path={ERoutes.Publications} element={<Publications/>}/>
          <Route path={ERoutes.CPThreshold} element={<CPThreshold/>}/>
          <Route path={ERoutes.Users} element={<Users/>}/>
          <Route path={ERoutes.Billing} element={<Billing/>}/>
        </Route>
      </Route>
    </Routes>
  )
}

export default RouteList;