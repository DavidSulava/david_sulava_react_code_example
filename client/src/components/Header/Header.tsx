import Profile from "./Profile/Profile"
import useAuthCheck from '../../helpers/hooks/useAuthCheck';
import React from 'react';
import BtnLink from '../BtnLink';
import { ERoutes } from '../../router/Routes';

const Header = () => {
  const {user, authenticated, authLoading} = useAuthCheck()
  return (
    <div className="header">
      <div className="header-left">
        <div className="logo-icon">DesignGear</div>
        <div>
          <BtnLink to={user? ERoutes.Organisations : ERoutes.Root}>
            <span className="k-icon k-i-home nav-icon"></span>
          </BtnLink>
        </div>

      </div>
      {
        authenticated && <Profile user={user}/>
      }
    </div>
  )
}
export default Header