export enum ERoutes {
  Root = '/',

  SignUp = '/signup',
  ForgotPwd = '/forgot-pwd',
  ResetPwd = '/passwordrecovery/:passwordRecoveryKey',

  Organisations = '/organisations',

  Dashboard = '/dashboard/:organizationId',
    ManageOrganisation = 'manage-organisation',
    Products = 'products',
      ProdVersions = '/dashboard/:organizationId/:productId/products',
      ProdVersion = '/dashboard/:organizationId/:productId/:versionId/products',
      ProdVersionConfig = '/dashboard/:organizationId/:productId/:versionId/:configId/products',
    Publications = 'publications',
    CPThreshold = 'cp-threshold',
    Users = 'users',
    Billing = 'billing',

  ManageAccount = '/manage-account',
    AccountSecurity = '/manage-account/account-security',
    AdminUserList = '/admin/users',
}
