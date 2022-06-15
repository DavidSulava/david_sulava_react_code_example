import BtnLink from '../../components/BtnLink';
import { ERoutes } from '../../router/Routes';
import { isNavLikActive } from '../../helpers/navLink';
import React, { useEffect } from 'react';
import { Outlet, useLocation } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { getAccount, setAccount } from '../../stores/authentication/reducer';

const ManageAccountPage = () => {
  const dispatch = useDispatch()
  const location = useLocation();

  useEffect(() => {
    dispatch(getAccount())
    return () => {
      dispatch(setAccount(null))
    }
  }, [])

  return (
    <div className="page-body">
      <div className="dash-board-header">
        <h6>
          Account management
        </h6>
      </div>
      <div className="dash-board-body account-body">
        <div className="dash-board-navigation account-navigation">
          <BtnLink
            to={ERoutes.ManageAccount}
            isActive={isNavLikActive(ERoutes.ManageAccount, location.pathname)}
            className='btn btn-outline-primary mb-2'
          >
            Personal details
          </BtnLink>
          <BtnLink
            to={ERoutes.AccountSecurity}
            isActive={isNavLikActive(ERoutes.AccountSecurity, location.pathname)}
            className='btn btn-outline-primary mb-2'
          >
            Security
          </BtnLink>
        </div>

        <div className="account-content">
          <Outlet/>
        </div>
      </div>
    </div>
  )
}

export default ManageAccountPage