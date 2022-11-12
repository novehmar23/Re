const { testsSucess } = require("./allTests.js");

async function allTests() {
    try {
        const tests = testsSucess();
        const testsAllSuccess = true;
        for (const currentTest of tests) {
            testsAllSuccess = testsAllSuccess && (await currentTest());
        }
        if (testsAllSuccess) {
            console.log("SUCCESS");
        } else {
            console.log("FAIL");
        }
    } catch (error) {} finally {
        console.log("FIN");
    }
}

allTests();



const testsSucess = () => {
    const allTests = [];
    allTests.push(test_Clasificacion_de_prioridad_de_un_bug_con_datos_validos);
    allTests.push(fake_test_2);
    return allTests;
    let test_1 = false;
    let test_2 = false;
    await test_Clasificacion_de_prioridad_de_un_bug_con_datos_validos().then(
        async(res) => {
            test_1 = res;
            await fake_test_2().then((res2) => {
                test_2 = res2;
                setTimeout(async function() {
                    console.log("FIN" + test_1 && test_2);
                    return test_1 && test_2;
                }, 10000);
            });
        }
    );
};