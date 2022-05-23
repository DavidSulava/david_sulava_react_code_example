export enum ERoutes {
  Root = '/',

  SignUp = '/signup',
  ForgotPwd = '/forgot-pwd',
  ResetPwd = '/reset-pwd',
  EmailConfirmation = '/email-confirmation',

  Organisations = '/organisations',

  Dashboard = '/dashboard/:organizationId',
  ProdVersion = '/dashboard/:organizationId/:productId',

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
