import CPThreshold from '../pages/DashboardPage/components/CPThreshold';

export enum ERoutes {
  Root = '/',

  SignIn = '/signin',
  SignUp = '/signup',
  SignOut = '/signout',
  ForgotPwd = '/forgot-pwd',
  ResetPwd = '/reset-pwd',
  EmailConfirmation = '/email-confirmation',

  Organisations = '/organisations',
  Dashboard = '/dashboard',
  ManageOrganisation = 'manage-organisation',
  Products = 'products',
  Publications = 'publications',
  CPThreshold = 'cp-threshold',
  Users = 'users',
  Billing = 'billing',

  UserProfile = '/user-profile',

  AdminUserList = '/admin/users',
  AdminUserEdit = '/admin/users/:id/edit',
}
