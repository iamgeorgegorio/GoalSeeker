import React, { useState } from 'react';
import axios from 'axios';
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import 'bootstrap/dist/css/bootstrap.min.css';


function Goalseek() {
    const [data, setData] = useState({
        formula: "",
        input: "",
        targetResult: "",
        maximumIterations: ""

    });

    const [displayOutput, setDisplayOutput] = useState<Record<string, unknown>>({});

    const handleChange = (e) => {
        const value = e.target.value;
        setData({
            ...data,
            [e.target.name]: value
        });
    };

    const handleSubmit = async (e: React.FormEventHandler<HTMLFormElement>) => {
        e.preventDefault();
        setDisplayOutput({});

        const goalseekdata = {
            formula: data.formula,
            input: data.input,
            targetResult: data.targetResult,
            maximumIterations: data.maximumIterations
        };

        try {
            const result = await axios.post('https://localhost:7186/api/GoalSeek/goalseek', goalseekdata)
            setDisplayOutput(result.data);
        } catch (error) {
            setDisplayOutput(error.message);
        }
  
    }

    return (
        <>
        <Form onSubmit={handleSubmit}>
            <Form.Group as={Row} className="mb-2" controlId="formHorizontalFormula">
                <Form.Label column sm={4} style={{ display: "flex", flexDirection: "row", alignItems: "center", }} >
                    Formula
                </Form.Label>
                <Col sm={8}>
                    <Form.Control type="text" name="formula" placeholder="formula" onChange={handleChange} value={data.formula} />
                </Col>
            </Form.Group>

            <Form.Group as={Row} className="mb-2" controlId="formHorizontalInputValue">
                <Form.Label column sm={4} style={{ display: "flex", flexDirection: "row", alignItems: "center", }} >
                    Input
                </Form.Label>
                <Col sm={8}>
                    <Form.Control type="text" name="input" placeholder="input value" onChange={handleChange} value={data.input} />
                </Col>
            </Form.Group>
            <Form.Group as={Row} className="mb-2" controlId="formHorizontalTargetResult">
                <Form.Label column sm={4} style={{ display: "flex", flexDirection: "row", alignItems: "center", }} >
                    Target Result
                </Form.Label>
                <Col sm={8}>
                    <Form.Control type="text" name="targetResult" placeholder="target result" onChange={handleChange} value={data.targetResult} />
                </Col>
            </Form.Group>
            <Form.Group as={Row} className="mb-2" controlId="formHorizontalMax">
                <Form.Label column sm={4} style={{ display: "flex", flexDirection: "row", alignItems: "center", }} >
                   MaxIteration
                </Form.Label>
                <Col sm={8}>
                    <Form.Control type="text" name="maximumIterations" placeholder="max iteration" onChange={handleChange} value={data.maximumIterations} />
                </Col>
            </Form.Group>

            <Button variant="primary" type="submit">
                Submit
            </Button>

            </Form>

            <section style={{textAlign:"left"}}>
                <pre>{JSON.stringify(displayOutput, null, 2)}</pre>
            </section>
        </>
    );
}

export default Goalseek;