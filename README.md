# 🚀 Affinidi Reference App

<div align="center">
  <img src="./docs/Affinidi%20Stacked_FC_RGB.jpg" alt="Affinidi Logo" width="180"/>
</div>


> **Welcome, Developer!**
> This project is a community-driven reference implementation for integrating **Affinidi Services** using the modern .NET 8.0 stack and Razor Pages.

> [!WARNING]
> This repository is intended for learning, experimentation, and prototyping only.
> **Do not use this code as-is in production environments.**
> Affinidi provides no warranty or guarantee for copy-paste usage.
> Please review, test, and secure your implementation before deploying to production.
> &nbsp;

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

Explore these features with step-by-step guides and official documentation:

- 🔐 **Affinidi Login ([OpenID4VP](https://openid.net/specs/openid-4-verifiable-presentations-1_0.html)) for Secure Passwordless Login**
  Easily enable passwordless authentication using verifiable presentations and Affinidi Login.
  - [Enable Affinidi Login in Reference App](./docs/setup-login-config.md)
  - [Affinidi Login Documentation](https://docs.affinidi.com/docs/affinidi-login/)


- 🔑 **Credential Issuance ([OpenID4VCI](https://openid.net/specs/openid-4-verifiable-credential-issuance-1_0.html))**
  Issue verifiable credentials using Affinidi's Credential Issuance Service.
  - [Enable Affinidi Credential Issuance Service in Reference App](./docs/cis-configuration.md)
  - [Affinidi Credential Issuance Service Documentation](https://docs.affinidi.com/docs/affinidi-elements/credential-issuance/)

- 🔗 **Credential Sharing ([OpenID4VP](https://openid.net/specs/openid-4-verifiable-presentations-1_0.html))**
  Share credentials securely with Affinidi Iota Framework.
  - [Enable Affinidi Iota framework in Reference App](./docs/setup-iota-config.md)
  - [Affinidi Iota Framework Documentation](https://docs.affinidi.com/docs/affinidi-elements/iota/)

- 🛡️ **Identity Verification (IDV)**
  Verify user identity using Affinidi’s IDV service.
  - [Enable Affinidi IDV in Reference App](./docs/setup-idv-config.md)
  - [Affinidi IDV Documentation](https://docs.affinidi.com/docs/affinidi-vault/identity-verification/)

- 🗄️ **Affinidi Vault Integration**
  Store and manage credentials securely in Affinidi Vault.
  - [Affinidi Vault Documentation](https://docs.affinidi.com/docs/affinidi-vault/)

- ✅ **Credential Verification**
  Verify credentials and presentations using Affinidi’s verification service.
  - [Affinidi Verification Documentation](https://docs.affinidi.com/docs/affinidi-elements/credential-verification/)

- ⚡ **Credential Revocation**
  Revoke credentials using Affinidi’s Revocation Service.
  - [Affinidi Revocation Documentation](https://docs.affinidi.com/docs/affinidi-elements/credential-issuance/revocation/)


For each feature, follow the linked setup guides to configure your environment and explore the documentation for deeper understanding and advanced usage.

## 🚀 Quickstart

**Prerequisites:**
- [.NET SDK 8.0](https://dotnet.microsoft.com/download)
- [Visual Studio Code](https://code.visualstudio.com/) or your favorite editor

**Check your .NET version:**
```sh
dotnet --version
# Should output: 8.0.xxx
```

**Get started:**
```sh
# Step 1: Clone the repository
> git clone https://github.com/Affinidi-Grajesh/dotnet-ref-app

# Step 2: Change directory to the cloned repository
> cd dotnet-ref-app

# Step 3: Copy the .env files
> cp .env.example .env

# Step 4: Update the .env file to enable the Affinidi feature in the reference app, follow the configuration provided above.

# Step 5: Build the Application
> dotnet build

# Step 6: Run the Application
> dotnet run
```
Visit [http://localhost:5068/](http://localhost:5068/) to explore the reference app!

## Read More

- [Affinidi Documentation](https://docs.affinidi.com/docs/)
- [Affinidi Labs](https://docs.affinidi.com/labs/)

## Telemetry

Affinidi collects usage data to improve our products and services.
See our [Privacy Notice](https://www.affinidi.com/privacy-notice) for details.


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

