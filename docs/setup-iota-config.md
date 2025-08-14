# Setup Iota Configuration

A framework that provides a secured and simplified data-sharing process from Affinidi Vault with user consent for enhanced user experience.
The Affinidi Iota Framework leverages the OID4VP (OpenID for Verifiable Presentation) standard to request and receive data from Affinidi Vault. The OID4VP is built with the OAuth 2.0 authorisation framework, providing developers with a simple and secure presentation of credentials.

## Create Iota configuration

When integrating with the Affinidi Iota Framework, developers must create a Configuration first, where they configure the Wallet used for signing the Request Token, the Request Token expiration to enhance security, and Presentation Definitions to query the data from the Affinidi Vault that users will consent to share.

1. Go to [Affinidi Portal](https://portal.affinidi.com/login) and click on the Affinidi Iota Framework page.

2. Click on Create Configuration and set the following fields:
  - Wallet: Create a new wallet and provide the new wallet name, or select an existing Wallet that will sign and issue the credentials to the user.
  *** Important - Only DID:Key is supported ***
  - Data Sharing flow mode - Redirect
  *** Important - Reference Implementation is with Redirect Flow, Websocket flow require Affinidi Login ***
  - Vault JWT Expiration time: Credential Offers have a limited lifetime to enhance security. Consumers must claim the offer within this timeframe.
  - Redirect URLs : http://localhost:5068/Iota

3. Optionally, you can configure whether to enable:

  - Enable Verification: To verify the credentials the user shares using the Credential Verification service.
  - Enable Consent Audit Log: To store the consent given by the user whenever they share data with the website.

4. After setting the fields and providing the list of the supported schema, click **Create**.

5. After creating the configuration, define the Presentation Definitions to query specific data from the Affinidi Vault. We will use Presentation Exchange to do this.

6. Create Presentations definitions for request below VC requests and [PEX is here](./avvanz-pex-query.json)
  - Address Verification VC
  - Personal Information Verification VC
  - Education Verification VC
  - Employment Verification VC
  - Selective Sharing VC



