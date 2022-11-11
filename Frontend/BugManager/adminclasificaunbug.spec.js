// Generated by Selenium IDE
const { Builder, By, Key, until } = require("selenium-webdriver");
const assert = require("assert");
const { describe } = require("jest");

describe("admin clasifica un bug", function() {
    this.timeout(30000);
    let driver;
    let vars;
    beforeEach(async function() {
        driver = await new Builder()
            .forBrowser("chrome")
            .usingServer("http://localhost:4444/wd/hub")
            .build();
        vars = {};
    });
    afterEach(async function() {
        await driver.quit();
    });
    it("admin clasifica un bug", async function() {
        // Test name: admin clasifica un bug
        // Step # | name | target | value
        // 1 | open | /login |
        await driver.get("http://localhost:4200/login");
        // 2 | setWindowSize | 1920x1040 |
        await driver.manage().window().setRect({ width: 1920, height: 1040 });
        // 3 | click | css=.mat-flat-button > .mat-button-wrapper |
        await driver
            .findElement(By.css(".mat-flat-button > .mat-button-wrapper"))
            .click();
        // 4 | click | css=.cdk-focused |
        await driver.findElement(By.css(".cdk-focused")).click();
        // 5 | click | id=mat-input-2 |
        await driver.findElement(By.id("mat-input-2")).click();
        // 6 | type | id=mat-input-2 | fake name
        await driver.findElement(By.id("mat-input-2")).sendKeys("fake name");
        // 7 | type | id=mat-input-3 | fake description
        await driver.findElement(By.id("mat-input-3")).sendKeys("fake description");
        // 8 | type | id=mat-input-4 | fake comment
        await driver.findElement(By.id("mat-input-4")).sendKeys("fake comment");
        // 9 | click | css=.ng-tns-c83-6 > .mat-form-field-infix |
        await driver
            .findElement(By.css(".ng-tns-c83-6 > .mat-form-field-infix"))
            .click();
        // 10 | click | css=#mat-option-2 > .mat-option-text |
        await driver
            .findElement(By.css("#mat-option-2 > .mat-option-text"))
            .click();
        // 11 | click | css=#save > .mat-button-wrapper |
        await driver.findElement(By.css("#save > .mat-button-wrapper")).click();
    });
});