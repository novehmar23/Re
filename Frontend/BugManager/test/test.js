const { By, Key, Builder } = require("selenium-webdriver");

require("chromedriver");

async function test_case() {
    let driver = await new Builder().forBrowser("chrome").build();

    await driver.get("https://google.com");

    await driver.findElement(By.name("q")).sendKeys("Hello world!", Key.RETURN);

    driver.quit();
}

test_case();

describe("tests que responden status code 200", function() {
    this.timeout(30000);
    let driver;
    beforeEach(async function() {
        driver = await new Builder()
            .forBrowser("chrome")
            .usingServer("http://localhost:4444/wd/hub")
            .build();
    });
    afterEach(async function() {
        await driver.quit();
    });
    it("Clasificación de prioridad de un bug con datos válidos", async function() {
        //Log in exitoso (como admin)
        await driver.get("http://localhost:4200/login");
        await driver.findElement(By.name("username")).sendKeys("admin");
        await driver.findElement(By.name("password")).sendKeys("admin");
        await driver.findElement(By.name("logInButton")).click();

        //Clasificación exitosa
        await driver
            .findElement(By.css(".mat-flat-button > .mat-button-wrapper"))
            .click();
        await driver.findElement(By.name("mat-input-2")).click();
        await driver.findElement(By.name("name")).sendKeys("fake name");
        await driver
            .findElement(By.name("description"))
            .sendKeys("fake description");
        await driver.findElement(By.name("comment")).sendKeys("fake comment");
        await driver.findElement(By.name("value")).sendKeys("Alta");
        await driver.findElement(By.id("save")).click();

        if (
            await driver
            .findElement(By.className("success ng-star-inserted"))
            .isDisplayed()
        ) {
            console.log("success");
        } else {
            console.log("fail");
        }
    });
});