# 🎓 Matric Advisor

> Helping South African matric students discover tertiary study opportunities based on their academic profile.

## Overview

Matric Advisor is a web application designed to simplify the transition from high school to higher education in South Africa.

Instead of manually searching through multiple university websites, students can enter their academic profile and receive a list of university programmes they are likely eligible for, along with entry requirements and important application deadlines.

The project aims to make higher education information more accessible while reducing the complexity of researching tertiary study options.

---

## The Problem

Every year, thousands of South African matric students must answer questions like:

- Which universities can I apply to?
- Which programmes do I qualify for?
- What APS score do I need?
- What subjects are required?
- When do applications close?

This information is often spread across multiple university websites, making the process time-consuming and confusing.

Matric Advisor centralises this information into one platform, helping students make informed decisions more efficiently.

---

## MVP Scope

The current version focuses on a single core user journey:

> A matric student enters their academic profile and receives a list of university programmes they may qualify for.

### Current Features

- Student profile creation
- APS score input
- Programme search
- University information
- Entry requirements
- Application deadlines
- Eligibility recommendations
- Save favourite programmes

---

## Future Roadmap

The long-term vision includes:

- Bursary discovery
- Scholarship opportunities
- Career guidance
- Application tracking
- Document management
- Email reminders
- Institution dashboards
- Student notifications

These features are intentionally outside the MVP to maintain a focused and achievable development scope.

---

## Tech Stack

### Backend

- C#
- ASP.NET Core Web API
- Entity Framework Core

### Database

- PostgreSQL

### Frontend

- React
- HTML5
- CSS3

### Tools

- Git
- GitHub
- Postman
- Visual Studio
- DBeaver

---

## Architecture

```text
React Frontend
       │
       ▼
ASP.NET Core Web API
       │
       ▼
Business Logic
       │
       ▼
Entity Framework Core
       │
       ▼
PostgreSQL Database
```

---

## Example User Journey

1. Student creates a profile.
2. Student enters:
   - APS Score
   - Subjects
   - Province
   - Preferred field of study
3. The system evaluates eligibility.
4. Matching university programmes are displayed.
5. Student saves preferred study options.

---

## Project Structure

```text
MatricConnect/

├── backend/
│   ├── Controllers/
│   ├── Models/
│   ├── Services/
│   ├── Data/
│   ├── DTOs/
│   └── Migrations/
│
├── frontend/
│   ├── components/
│   ├── pages/
│   ├── services/
│   └── assets/
│
└── README.md
```

---

## Database (Initial Entities)

- Student
- University
- Programme
- ProgrammeRequirement
- SavedProgramme

---

## Development Status

🚧 In Active Development

Current milestone:

- Project planning
- Database design
- Backend architecture
- MVP implementation

---

## Design Principles

- Simplicity over feature overload
- Real-world usability
- Scalable architecture
- Clean code
- Separation of concerns
- Mobile-friendly design

---

## Vision

Matric Advisor aims to become a trusted platform that helps South African learners confidently navigate their journey from high school into higher education.

The initial MVP focuses on university programme discovery, with future expansion into bursaries, applications, and student support services.

---

## Author

**Lerato Gladys**

Student Software Developer

Portfolio:
https://github.com/Leratogladys

LinkedIn:
https://www.linkedin.com/in/lerato-molefe-7403891b7


