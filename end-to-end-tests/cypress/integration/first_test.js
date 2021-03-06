import '@testing-library/cypress/add-commands';

describe('My First Test', function () {
  before(() => {
    cy.task('cleanTable', 'User')
  })

  it('Connect to the main page', function () {
    const fakestring='cyril9'
    // const base_url='http://localhost:4000'
    const base_url = Cypress.config().baseUrl
    // Go to home page
    cy.visit('')
    cy.url()
      .should('equal', base_url+'/user')

    // Go to create user
    cy.findByTestId('create-user-link').should('exist')
    cy.findByTestId('create-user-link').click()
    cy.url().should('equal', base_url+'/user/register')

    // Create user
    cy.get('[id="firstname-field"]').type(fakestring)
    cy.get('[id="lastname-field"]').type(fakestring)
    cy.get('[id="login-field"]').type(fakestring)
    cy.get('[id="password-field"]').type(fakestring)
    cy.get('[id="save-button"]').click()
    // cy.url().should('equal', base_url+'/user')

    //
    cy.findByTestId('login-field').type(fakestring)
    cy.findByTestId('password-field').type(fakestring)
    cy.findByTestId('signin-button').click()
    cy.url()
      .should('equal', `${base_url}/`)

    //
    cy.get('[id="project-icon"]').click()
    cy.url()
      .should('include', '/projects')

    //
    cy.get('[id="algorithm-icon"]').click()
    cy.url()
      .should('include', '/algorithms')

    //
    cy.get('[id="home-icon"]').click()
    cy.url()
      .should('include', '/home')

    //
    cy.get('[id="series-icon"]').click()
    cy.get('[id="synchronize-pacs-icon"]').click()
    cy.url()
      .should('include', '/series')
  })
})