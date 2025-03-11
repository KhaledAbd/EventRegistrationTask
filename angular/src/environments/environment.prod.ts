import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'EventRegistrationTask',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44354/',
    redirectUri: baseUrl,
    clientId: 'EventRegistrationTask_App',
    responseType: 'code',
    scope: 'offline_access EventRegistrationTask',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44354',
      rootNamespace: 'RAG.EventRegistrationTask',
    },
  },
} as Environment;
