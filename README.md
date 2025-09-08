# <Project Name>

<div align="center">
  <img src="./docs/Affinidi%20Stacked_FC_RGB.jpg" alt="Banner or Logo" width="180"/>
</div>

> A reference application showcasing how to integrate **Affinidi Services** using **Affinidi Tech Stack**.

> This project is a .NET 8.0 Razor web app, utilizing the power of Razor Pages for building dynamic web pages.


## 📖 Table of Contents
- [Overview](#-overview)
- [Features to Explore](#-features-to-explore)
- [Quickstart](#-quickstart)
- [Read More](#read-more)
- [Telemetry](#telemetry)
- [Feedback, Support, and Community](#feedback-support-and-community)
- [FAQ](#faq)
- [Disclaimer](#_disclaimer_)



## 🧭 Overview
This proreference application demonstrates how to use **Affinidi Services** to **issue verifiable credentials and store verifiable credentials in Affinidi Vault**.

Additonaly, It also show capabilites for **Affinidi Iota Framwork (Sharing), Affinidi Identity Verification (IDV), Affinidi Verification, Affinidi Revocation**.

It is built with **Dot Net 8.0** and **Razor Pages** and serves as a **reference implementation** for developers who want to:
- Learn the **Affinidi APIs & TDK(Trust Development Kit)**
- Bootstrap their own integrations
- Explore credential **issuance, verification, Iota, IDV and revocation** flows


## ✨ Features to Explore

Below are the main features you can try in this reference app. Each feature links to the official Affinidi documentation and setup guides for a smooth onboarding experience:

- 🔑 **Credential Issuance ([OpenID4VCI](https://openid.net/specs/openid-4-verifiable-credential-issuance-1_0.html))**
  Issue verifiable credentials using Affinidi's Credential Issuance Service.
  - 👉 [Setup Issuance Configuration](./docs/cis-configuration.md)
  - 👉 [Affinidi Credential Issuance Docs](https://docs.affinidi.com/docs/affinidi-elements/credential-issuance/)

- 🔗 **Credential Sharing ([OpenID4VP](https://openid.net/specs/openid-4-verifiable-presentations-1_0.html))**
  Share credentials securely using Affinidi Iota Framework.
  - 👉 [Setup Iota Configuration](./docs/setup-iota-config.md)
  - 👉 [Affinidi Iota Docs](https://docs.affinidi.com/docs/affinidi-elements/iota/)

- 🛡️ **Identity Verification (IDV)**
  Verify user identity using Affinidi's IDV service.
  - 👉 [Setup IDV Configuration](./docs/setup-idv-config.md)
  - 👉 [Affinidi IDV Docs](https://docs.affinidi.com/docs/affinidi-vault/identity-verification/)

- 🗄️ **Secure Storage in Affinidi Vault**
  Store and manage credentials securely in Affinidi Vault.
  - 👉 [Affinidi Vault Docs](https://docs.affinidi.com/docs/affinidi-vault/)

- ✅ **Credential Verification**
  Verify Verifiable Credentials and Presentations using Affinidi's verification service.
  - 👉 [Affinidi Credential Verification Docs](https://docs.affinidi.com/docs/affinidi-elements/credential-verification/)

- ⚡ **Credential Revocation**
  Revoke credentials using Affinidi's Revocation Service.
  - 👉 [Affinidi Revocation Docs](https://docs.affinidi.com/docs/affinidi-elements/credential-issuance/revocation/)


For each feature, follow the linked setup guides to configure your environment and explore the documentation for deeper understanding and advanced usage.

## 🚀 Quickstart
Make sure you have the following tools installed on your machine:

- [.NET SDK 8.0](https://dotnet.microsoft.com/download)
- [Visual Studio code](https://code.visualstudio.com/) (or any preferred code editor)

### PreRequisite check

Run this command to get the Dotnet version
    ```sh
    dotnet --version
    ```

You should get the output as below, please reinstall dotnet if the version is not 8.0.xxx
    ```sh
    8.0.XXX
    ```

```sh
# 1. Clone the repo
git clone https://github.com/affinidi/<repo-name>.git
cd <repo-name>

# 2. Copy environment variables
cp .env.example .env

# 3. Build and run the project:

dotnet build
dotnet run
```

> Visit: http://localhost:5068/ to browse the reference app

## Affinidi Configurations

1. Click here to [Set up your environment variables for Affinidi Login configuration](./docs/setup-login-config.md)
2. Click here to [Set up your Personnel Access Token to interact with Affinidi services](./docs/create-pat.md)
3. Click here to [Set up your Credential Issuance Configuration](./docs/cis-configuration.md)
4. Click here to [Set up your environment variables for Affinidi Iota configuration](./docs/setup-iota-config.md)
5. Click here to [Set up your environment Variables for Affinidi IDV Configuration](./docs/setup-idv-config.md)

## Read More

Explore our [documentation](https://docs.affinidi.com/docs/) and [labs](https://docs.affinidi.com/labs/) to learn more about integrating Affinidi Login with Affinidi Vault.

## Telemetry

Affinidi collects usage data to improve our products and services. For information on what data we collect and how we use your data, please refer to our [Privacy Notice](https://www.affinidi.com/privacy-notice).

## Feedback, Support, and Community

[Click here](https://github.com/affinidi/reference-app-affinidi-vault/issues) to create a ticket and we will get on it right away. If you are facing technical or other issues, you can [Contact Support](https://share.hsforms.com/1i-4HKZRXSsmENzXtPdIG4g8oa2v).


## FAQ

### What can I develop?

You are only limited by your imagination! Affinidi Reference Applications is a toolbox with which you can build software apps for personal or commercial use.

### Is there anything I should not develop?

We only provide the tools - how you use them is largely up to you. We have no control over what you develop with our tools - but please use our tools responsibly!

We hope that you will not develop anything that contravenes any applicable laws or regulations. Your projects should also not infringe on Affinidi’s or any third party’s intellectual property (for instance, misusing other parties’ data, code, logos, etc).

### What responsibilities do I have to my end-users?

Please ensure that you have in place your own terms and conditions, privacy policies, and other safeguards to ensure that the projects you build are secure for your end users.

If you are processing personal data, please protect the privacy and other legal rights of your end-users and store their personal or sensitive information securely.

Some of our components would also require you to incorporate our end-user notices into your terms and conditions.

### Are Affinidi Reference Applications free for use?

Affinidi Reference Applications are free, so come onboard and experiment with our tools and see what you can build! We may bill for certain components in the future, but we will inform you beforehand.

### Do I need to provide you with anything?

From time to time, we may request certain information from you to ensure that you are complying with the [Terms and Conditions](https://www.affinidi.com/terms-conditions).

### Can I share my developer’s account with others?

When you create a developer’s account with us, we will issue you your private login credentials. Please do not share this with anyone else, as you would be responsible for activities that happen under your account. If you have friends who are interested, ask them to sign up – let's build together!

## _Disclaimer_

_Please note that this FAQ is provided for informational purposes only and is not to be considered a legal document. For the legal terms and conditions governing your use of the Affinidi Reference Applications, please refer to our [Terms and Conditions](https://www.affinidi.com/terms-conditions)._

