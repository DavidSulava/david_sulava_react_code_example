import Profile from "./Profile/Profile"
import useAuthCheck from '../../helpers/hooks/useAuthCheck';

const Header = () => {
  const {user, authenticated, checkingAuth} = useAuthCheck()
  return (
    <div className="header">
      <div className="logo-icon">DesignGear</div>
      {
        authenticated && <Profile user={user}/>
      }
    </div>
  )
}
export default Header