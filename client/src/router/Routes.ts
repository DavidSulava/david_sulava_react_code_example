export enum ERoutes {
  Root = '/',

  SignUp = '/signup',
  ForgotPwd = '/forgot-pwd',
  ResetPwd = '/reset-pwd',
  EmailConfirmation = '/email-confirmation',

  Organisations = '/organisations',

  Dashboard = '/dashboard/:organizationId',
  ProdVersions = '/dashboard/:organizationId/:productId/products',
  ProdVersion = '/dashboard/:organizationId/:productId/:versionId/products',

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
